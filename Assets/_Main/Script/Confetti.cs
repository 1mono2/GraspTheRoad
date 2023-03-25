using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Confetti : MonoBehaviour
{
    [SerializeField] ParticleSystem _confetti;

    private void Start()
    {
        LevelPresenter.I.GoalCounter.Count
            .Skip(1)
            .Subscribe(_ => _confetti.Play()).AddTo(this);
    }
}
