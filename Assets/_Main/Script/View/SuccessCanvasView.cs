using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UniRx;
using DG.Tweening;
using UnityEngine.UI;

public class SuccessCanvasView : MonoBehaviour
{
    [SerializeField] private float _delayDuration = 0.7f;
    
    [SerializeField] private GameObject _successText;
    private Vector3 _defaultScale;
    [SerializeField] private ParticleSystem _confetti;
    [SerializeField] private GameObject _nextButton;
    [SerializeField] private Image _backgroundImage;
    private void Start()
    {
        _defaultScale = _successText.transform.localScale;
        _successText.transform.localScale = Vector3.zero;
        
        LevelPresenter.I.LevelProgressState
            .Where(state => state == StateType.Success)
            .Delay(TimeSpan.FromSeconds(1))
            .Subscribe(_ =>
            {
                OnSuccess();
            }).AddTo(this);
    }

    private async void OnSuccess()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_delayDuration));
        
        ShowNextButton();
        ShowSuccessText();
        ShowBackgroundImage();
        _confetti.Play();
    }
    
    private void ShowNextButton()
    {
        _nextButton.SetActive(true);
        _nextButton.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack);
    }
    
    private void ShowBackgroundImage()
    {
        _backgroundImage.gameObject.SetActive(true);
        _backgroundImage.DOFade(0.5f, 0.5f).SetEase(Ease.OutBack); 
    }
    
    private void ShowSuccessText()
    {
        _successText.SetActive(true);
        _successText.transform.DOScale(_defaultScale, 0.5f).SetEase(Ease.OutBack);
    }
}
