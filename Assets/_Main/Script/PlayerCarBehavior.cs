
using UnityEngine;
using UniRx;
using UniRx.Triggers;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCarBehavior : AbstractCarBehavior
{
    protected virtual void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        LevelPresenter.I.LevelProgressState
            .Where(state => state != StateType.StartView)
            .Subscribe(_ => Move()).AddTo(this);
        
        _bodyCollider.OnTriggerEnter2DAsObservable()
            .Where(collision => collision.gameObject.CompareTag("GoalFlag") && !_isGoal && _isCrashed == false)
            .Subscribe(collision =>
            {
                LevelPresenter.I.GoalCounter.AddCount(1);
                _isGoal = true;
            }).AddTo(this);
        
        _bodyCollider.OnCollisionEnter2DAsObservable()
            .Where(collision => collision.gameObject.TryGetComponent<IObstacle>(out var obstacle) || 
                                collision.gameObject.TryGetComponent<OppositeCar>(out var carBehavior))
            .Take(1)
            .Subscribe(collision =>
            {
                Crash();
                
            }).AddTo(this);
        
        this.transform.ObserveEveryValueChanged(transform => transform.position)
            .Where(position => position.y < -20f)
            .Subscribe(_ => Destroy(this.gameObject)).AddTo(this);
        
        _gasEffect.Play();
        //PlayHornSound();
    }

}
