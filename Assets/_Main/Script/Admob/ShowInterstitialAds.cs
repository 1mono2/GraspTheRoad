using UnityEngine;
using UnityEngine.SceneManagement;
using MoNo.Utility;
using TMPro;
using UniRx;
//
//
public class ShowInterstitialAds : MonoBehaviour
{
//     [SerializeField] TextMeshProUGUI _text;
//
//     public void Start()
//     {
//         LevelPresenter.I.LevelProgressState
//             .Where(state => state == StateType.Success)
//             //.Where(_ => SceneManager.GetActiveScene().buildIndex % 2 == 0)
//             .Subscribe(_ => { ShowAds(); }).AddTo(this);
//         
//     }
//
//     public void ShowAds()
//     {
//         if (InterstitialAds.I != null)
//         {
//             _text.text = InterstitialAds.I.ShowIfLoaded() ? "ShowAds" : "NotShowAds";
//         }
//         else
//         {
//             Debug.LogWarning("InterstitialAds is null");
//             _text.text = "InterstitialAds is null";
//         }
//     }
}