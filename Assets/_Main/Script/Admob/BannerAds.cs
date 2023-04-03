// using System;
// using UnityEngine;
// using UnityEngine.Events;
// using GoogleMobileAds.Api;
// using UnityEngine.SceneManagement;
//
// namespace MoNo.Utility
// {
//     public class BannerAds : SingletonMonoBehaviour<BannerAds>
//     {
//         protected override bool DontDestroy => true;
//
//
//         [SerializeField] string _adUnitIdAndroid = "ca-app-pub-3940256099942544/6300978111";
//         [SerializeField] string _adUnitIdiOS = "ca-app-pub-3940256099942544/2934735716";
//         
//         [System.Serializable] public class Handler : UnityEvent { }
//         public Handler OnAdLoaded;
//         public Handler OnAdFailedToLoad;
//         public Handler OnAdClicked;
//         public Handler OnAdPaid;
//         
//         // Ads
//         [Header("Persistent Banner")]
//         [SerializeField]private bool _persistent = true;
//         private BannerView _bannerView;
//         private Canvas _bannerCanvas;
//         
//
//         public void Initialize()
//         {
//             LoadBannerAd();
//         }
//         
//         public void LoadBannerAd()
//         {
//             // Initialize instance
//             this._bannerView?.Destroy();
//             if(_bannerCanvas != null)
//                 Destroy(_bannerCanvas.gameObject);
//
// #if UNITY_ANDROID
//             string adUnitId = _adUnitIdAndroid;
// #elif UNITY_IPHONE
//             string adUnitId = _adUnitIdiOS;
// #else
//         string adUnitId = "unexpected_platform";
// #endif
//
//             AdSize adaptiveSize =
//                 AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
//
//             _bannerView = new BannerView(adUnitId, adaptiveSize, AdPosition.Bottom);
//             AdRequest adRequest = new AdRequest.Builder().Build();
//             AddHandle();
//             // Load a banner ad.
//             _bannerView.LoadAd(adRequest);
//
//         }
//
//         private void AddHandle()
//         {
//             this._bannerView.OnBannerAdLoaded += HandleOnAdLoaded;
//             this._bannerView.OnBannerAdLoadFailed += HandleOnAdFailedToLoad;
//             this._bannerView.OnAdClicked += HandleOnAdClicked;
//             this._bannerView.OnAdPaid += HandleOnPaid;
//         }
//
//         private void HandleOnAdLoaded()
//         {
//             OnAdLoaded.Invoke();
//             if(_persistent)
//             {
//                 _bannerCanvas = GameObject.Find("ADAPTIVE(Clone)").GetComponent<Canvas>();
//                 DontDestroyOnLoad(_bannerCanvas.gameObject);
//             }
//             
//             Debug.Log("Banner ad has succeed to load ad.");
//         }
//
//
//         private void HandleOnAdFailedToLoad(AdError error)
//         {
//             OnAdFailedToLoad.Invoke();
//             Debug.Log("Banner ad has failed to load ad. : " + error);
//         }
//
//         private void HandleOnAdClicked()
//         {
//             OnAdClicked.Invoke();
//             Debug.Log("Banner ad has clicked.");
//         }
//         
//         private void HandleOnPaid(AdValue adValue)
//         {
//             OnAdPaid.Invoke();
//             Debug.Log("Banner ad has earned " + adValue.Value + " " + adValue.CurrencyCode + ".");
//         }
//         
//
//
//         public void Dispose()
//         {
//             if (this._bannerView == null) return;
//             
//             this._bannerView.OnBannerAdLoaded -= HandleOnAdLoaded;
//             this._bannerView.OnBannerAdLoadFailed -= HandleOnAdFailedToLoad;
//             this._bannerView.OnAdClicked -= HandleOnAdClicked;
//             this._bannerView.OnAdPaid -= HandleOnPaid;
//             
//             this._bannerView?.Destroy();
//             this._bannerView = null;
//         }
//
//         void OnDestroy()
//         {
//             Dispose();
//         }
//
//     }
// }