// using System;
// using UnityEngine;
// using UnityEngine.Events;
// using GoogleMobileAds.Api;
//
//
// namespace MoNo.Utility
// {
//     public class InterstitialAds : SingletonMonoBehaviour<InterstitialAds>
//     {
//         protected override bool DontDestroy => true;
//
//
//         [SerializeField] string _adUnitIdAndroid = "ca-app-pub-3940256099942544/1033173712";
//         [SerializeField] string _adUnitIdiOS = "ca-app-pub-3940256099942544/4411468910";
//
//
//         // Ads
//         private InterstitialAd _interstitialAd;
//
//         [System.Serializable]
//         public class Handler : UnityEvent
//         {
//         }
//         
//         public Handler OnAdLoaded;
//         public Handler OnAdFailedToLoad;
//         public Handler OnAdFullScreenContentOpened;
//         public Handler OnAdFullScreenContentClosed;
//         public Handler OnAdFullScreenContentFailed;
//         public Handler OnAdImpressionRecorded;
//         public Handler OnAdPaid;
//         public Handler OnAdClicked;
//         
//
//         public void Initialize()
//         {
//
//             LoadInterstitialAd();
//         }
//
//         private void LoadInterstitialAd()
//         {
//             // Initialize instance
//             if (this._interstitialAd != null)
//             {
//                 this._interstitialAd?.Destroy();
//                 this._interstitialAd = null;
//             }
//
// #if UNITY_ANDROID
//             string adUnitId = _adUnitIdAndroid;
// #elif UNITY_IPHONE
//             string adUnitId = _adUnitIdiOS;
// #else
//         string adUnitId = "unexpected_platform";
// #endif
//
//             // Create an empty ad request.
//             AdRequest request = new AdRequest.Builder().Build();
//             // Load the interstitial with the request.
//             InterstitialAd.Load(adUnitId,request,
//                 (InterstitialAd ad, LoadAdError error) =>
//                 {
//                     // if error is not null, the load request failed.
//                     if (error != null || ad == null)
//                     {
//                         Debug.LogError("interstitial ad failed to load an ad " +
//                                        "with error : " + error);
//                         OnAdFailedToLoad.Invoke();
//                         return;
//                     }
//
//                     Debug.Log("Interstitial ad loaded with response : "
//                               + ad.GetResponseInfo());
//                     OnAdLoaded.Invoke();
//                     _interstitialAd = ad;
//                 });
//             RegisterEventHandle();
//
//
//         }
//
//         public bool ShowIfLoaded()
//         {
//             if (this._interstitialAd.CanShowAd())
//             {
//                 this._interstitialAd.Show();
//                 return true;
//             }
//
//             Debug.Log("don't loaded");
//             return false;
//         }
//
//         private void RegisterEventHandle()
//         {
//             if (this._interstitialAd == null)
//             {
//                 Debug.LogWarning("Interstitial ad is null");
//                 return;
//             }
//                 
//             this._interstitialAd.OnAdFullScreenContentOpened += HandleOnAdOpening;
//             this._interstitialAd.OnAdFullScreenContentClosed += HandleOnAdClosed;
//             this._interstitialAd.OnAdFullScreenContentFailed += HandleOnAdFullScreenContentFailed;
//         }
//         
//
//         private void HandleOnAdOpening()
//         {
//             OnAdFullScreenContentOpened.Invoke();
//             Debug.Log("Interstitial ad has opened.");
//         }
//
//         private void HandleOnAdClosed()
//         {
//             OnAdFullScreenContentClosed.Invoke();
//             Debug.Log("Interstitial ad has closed");
//
//             // When close Ads, create next one;
//             Dispose();
//             LoadInterstitialAd();
//         }
//         
//         private void HandleOnAdFullScreenContentFailed(AdError error)
//         {
//             OnAdFullScreenContentFailed.Invoke();
//             Debug.Log("Interstitial ad has failed to show ad.: " + error);
//             Dispose();
//             LoadInterstitialAd();
//         }
//
//         public void Dispose()
//         {
//             if(_interstitialAd == null) return;
//             this._interstitialAd.OnAdFullScreenContentOpened -= HandleOnAdOpening;
//             this._interstitialAd.OnAdFullScreenContentClosed -= HandleOnAdClosed;
//             this._interstitialAd.OnAdFullScreenContentFailed -= HandleOnAdFullScreenContentFailed;
//             this._interstitialAd?.Destroy();
//             this._interstitialAd = null;
//         }
//
//         void OnDestroy()
//         {
//             Dispose();
//         }
//     }
// }