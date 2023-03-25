using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoNo.Utility;
using UniRx;


public class ShowInterstitialAds : MonoBehaviour
{
    public void Start()
    {
        LevelPresenter.I.LevelProgressState
            .Where(state => state == StateType.Success)
            .Subscribe(_ =>
            {
                ShowAds();
            }).AddTo(this);
    }

    public void ShowAds()
    {
        if (SceneManager.GetActiveScene().buildIndex % 2 == 0)
        {
            if (InterstitialAds.I != null)
            {
                InterstitialAds.I.ShowIfLoaded();
            }else
                Debug.Log("InterstitialAds is null");
        }
    }
}