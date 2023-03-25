using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine.VFX;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class OppositeCar : AbstractCarBehavior
{
    private void Start()
    {
        this._rigidbody = GetComponent<Rigidbody2D>();
        LevelPresenter.I.LevelProgressState
            .Where(state => state != StateType.StartView)
            .Subscribe(_ => Move()).AddTo(this);
        
        
        _bodyCollider.OnCollisionEnter2DAsObservable()
            .Where(collision => collision.gameObject.TryGetComponent<IObstacle>(out var obstacle) || 
                                collision.gameObject.TryGetComponent<PlayerCarBehavior>(out var carBehavior))
            .Take(1)
            .Subscribe(collision =>
            {
                Crash();
                
            }).AddTo(this);
        
        this.transform.ObserveEveryValueChanged(transform => transform.position)
            .Where(position => position.y < -20f)
            .Subscribe(_ => Destroy(this.gameObject)).AddTo(this);
        
        _gasEffect.Play();
    }
}
