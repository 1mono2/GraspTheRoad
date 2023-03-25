using System;
using UnityEngine;
using UnityEngine.Events;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

namespace MoNo.Utility
{
    public class BannerAds : SingletonMonoBehaviour<BannerAds>
    {
        protected override bool DontDestroy => true;


        [SerializeField] string _adUnitIdAndroid = "ca-app-pub-3940256099942544/6300978111";
        [SerializeField] string _adUnitIdiOS = "ca-app-pub-3940256099942544/2934735716";
        [System.Serializable] public class Provider : UnityEvent { }
        public Provider OnAdLoaded;
        public Provider OnAdFailedToLoad;
        public Provider OnAdOpening;
        public Provider OnAdClosed;
        
        // Ads
        [Header("Persistent Banner")]
        [SerializeField]private bool _persistent = true;
        private BannerView _bannerView;
        private Canvas _bannerCanvas;
        

        void Start()
        {
            RequestBanner();
        }
        
        public void RequestBanner()
        {
            // Initialize instance
            this._bannerView?.Destroy();
            if(_bannerCanvas != null)
                Destroy(_bannerCanvas.gameObject);

#if UNITY_ANDROID
            string adUnitId = _adUnitIdAndroid;
#elif UNITY_IPHONE
            string adUnitId = _adUnitIdiOS;
#else
        string adUnitId = "unexpected_platform";
#endif

            AdSize adaptiveSize =
                AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

            _bannerView = new BannerView(adUnitId, adaptiveSize, AdPosition.Bottom);
            AdRequest adRequest = new AdRequest.Builder().Build();
            AddHandle();
            // Load a banner ad.
            _bannerView.LoadAd(adRequest);
            //_bannerView.Hide();

        }
        [ContextMenu("Show")]
        public void Show()
        {
            _bannerView.Show();
        }
        
        [ContextMenu("Hide")]
        public void Hide()
        {
            _bannerView.Hide();
        }

        private void AddHandle()
        {
            this._bannerView.OnAdLoaded += HandleOnAdLoaded;
            this._bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            this._bannerView.OnAdOpening += HandleOnAdOpening;
            this._bannerView.OnAdClosed += HandleOnAdClosed;
            this._bannerView.OnPaidEvent += HandleOnPaidEvent;
        }

        private void HandleOnAdLoaded(object sender, EventArgs args)
        {
            OnAdLoaded.Invoke();
            if(_persistent)
            {
                _bannerCanvas = GameObject.Find("ADAPTIVE(Clone)").GetComponent<Canvas>();
                DontDestroyOnLoad(_bannerCanvas.gameObject);
            }
            
            Debug.Log("Banner ad has succeed to load ad.");
        }


        private void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            OnAdFailedToLoad.Invoke();
            Debug.Log("Banner ad has failed to load ad.");
        }

        private void HandleOnAdOpening(object sender, EventArgs args)
        {
            OnAdOpening.Invoke();
            Debug.Log("Banner ad has opened.");
        }

        private void HandleOnAdClosed(object sender, EventArgs args)
        {
            OnAdClosed.Invoke();
            Debug.Log("Banner ad has closed");
        }
        
        /// <summary>
        /// I don't know what this is.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleOnPaidEvent(object sender, AdValueEventArgs e)
        {
            Debug.Log("Paid event received");
        }
        


        public void Dispose()
        {
            this._bannerView.OnAdLoaded -= HandleOnAdLoaded;
            this._bannerView.OnAdFailedToLoad -= HandleOnAdFailedToLoad;
            this._bannerView.OnAdOpening -= HandleOnAdOpening;
            this._bannerView.OnAdClosed -= HandleOnAdClosed;
            this._bannerView.OnPaidEvent -= HandleOnPaidEvent;
            
            this._bannerView.Destroy();
        }

        void OnDestroy()
        {
            if (this._bannerView != null)
            {
                Dispose();
            }
        }

    }
}