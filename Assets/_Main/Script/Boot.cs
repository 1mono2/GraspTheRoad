using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using GoogleMobileAds.Api;
//using GoogleMobileAds.Common;
using MoNo.Utility;

public class Boot : MonoBehaviour
{
    [SerializeField] bool _isShowAd = true;
    public bool isShowAd => _isShowAd;
    const string SAVE_STAGE_INDEX = "StageIndex";

    private void Awake()
    {
        InitializeAdmob();
    }


    void InitializeAdmob()
    {
        if (_isShowAd == false) return;

        // admob initialize
        // MobileAds.Initialize(initStatus =>
        // {
        //     // Setting the same app key.
        //     RequestConfiguration requestConfiguration = new RequestConfiguration.Builder()
        //         .build();
        //     MobileAds.SetRequestConfiguration(requestConfiguration);
        //     // AdMobからのコールバックはメインスレッドで呼び出される保証がないため、次のUpdate()で呼ばれるようにMobileAdsEventExecutorを使用
        //     //MobileAdsEventExecutor.ExecuteInUpdate();
        // });
    }
    
    private void Start()
    {
        // BannerAds.I.Initialize();
        // InterstitialAds.I.Initialize();
        LoadScene();
    }

    public static void LoadScene()
    {
        // Load stage index.
        if (!PlayerPrefs.HasKey(SAVE_STAGE_INDEX))
        {
            SceneManager.LoadScene(1);
            return;
        }

        int savedSceneNum = PlayerPrefs.GetInt(SAVE_STAGE_INDEX);
        if (savedSceneNum < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(savedSceneNum + 1);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
}