using System;
using UnityEngine;
using UnityEngine.Events;
using GoogleMobileAds.Api;


namespace MoNo.Utility
{
    
    public class InterstitialAds : SingletonMonoBehaviour<InterstitialAds>
    {
        protected override bool DontDestroy => true;


        [SerializeField] string _adUnitIdAndroid = "ca-app-pub-3940256099942544/1033173712";
        [SerializeField] string _adUnitIdiOS = "ca-app-pub-3940256099942544/4411468910";


        // Ads
        private InterstitialAd _interstitialAd;

        [System.Serializable]
        public class Provider : UnityEvent
        {
        }

        public Provider OnAdLoaded;
        public Provider OnAdFailedToLoad;
        public Provider OnAdOpening;
        public Provider OnAdClosed;

        //const string SAVE_PURCHASING_AD_FLAG = "PurchasingAdFlag"; // 1:purchased 0: NOT purchased

        void Start()
        {
            // if (PlayerPrefs.GetInt(SAVE_PURCHASING_AD_FLAG) == 1)
            // {
            //     Destroy(this);
            //     return;
            // }


            CreateAndLoadRewardAd();
        }

        private void CreateAndLoadRewardAd()
        {
            // Initialize instance
            this._interstitialAd?.Destroy();

#if UNITY_ANDROID
            string adUnitId = _adUnitIdAndroid;
#elif UNITY_IPHONE
            string adUnitId = _adUnitIdiOS;
#else
        string adUnitId = "unexpected_platform";
#endif

            // Initialize an InterstitialAd.
            this._interstitialAd = new InterstitialAd(adUnitId);


            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the interstitial with the request.
            this._interstitialAd.LoadAd(request);


            AddHandle();
        }

        public bool ShowIfLoaded()
        {
            if (this._interstitialAd.IsLoaded())
            {
                this._interstitialAd.Show();
                return true;
            }
            else
            {
                Debug.Log("don't loaded");
                return false;
            }
        }

        private void AddHandle()
        {
            this._interstitialAd.OnAdLoaded += HandleOnAdLoaded;
            this._interstitialAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            this._interstitialAd.OnAdOpening += HandleOnAdOpening;
            this._interstitialAd.OnAdClosed += HandleOnAdClosed;
        }


        private void HandleOnAdLoaded(object sender, EventArgs args)
        {
            OnAdLoaded.Invoke();
            Debug.Log("Interstitial ad has successed to load ad.");
        }


        private void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            OnAdFailedToLoad.Invoke();
            Debug.Log("Interstitial ad has failed to load ad.");
        }

        private void HandleOnAdOpening(object sender, EventArgs args)
        {
            OnAdOpening.Invoke();
            Debug.Log("Interstitial ad has opened.");
        }

        private void HandleOnAdClosed(object sender, EventArgs args)
        {
            OnAdClosed.Invoke();
            Debug.Log("Interstitial ad has closed");

            // When close Ads, create next one;
            Dispose();
            CreateAndLoadRewardAd();
        }
        


        public void Dispose()
        {
            this._interstitialAd.OnAdLoaded -= HandleOnAdLoaded;
            this._interstitialAd.OnAdFailedToLoad -= HandleOnAdFailedToLoad;
            this._interstitialAd.OnAdOpening -= HandleOnAdOpening;
            this._interstitialAd.OnAdClosed -= HandleOnAdClosed;
        }

        void OnDestroy()
        {
            if (this._interstitialAd != null)
            {
                Dispose();
            }
        }
    }
}