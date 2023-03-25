using System;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.VFX;
using DG.Tweening;
using Cysharp.Threading.Tasks;

[RequireComponent(typeof(Rigidbody2D))]
public class AbstractCarBehavior: MonoBehaviour
{
    [SerializeField] protected float _speed = 1f;
    
    [SerializeField] protected GameObject _body;
    [SerializeField] protected GameObject _wheel;
    [SerializeField] protected Collider2D _bodyCollider;
    [SerializeField] protected VisualEffect _gasEffect;
    [SerializeField] protected ParticleSystem _explosionPrefab;
    [SerializeField] protected AudioSource _hornSound;
    [SerializeField] protected AudioSource _crashSound;
    
    
    protected Rigidbody2D _rigidbody;
    protected IDisposable _move;
    protected bool _isCrashed = false;
    protected bool _isGoal = false;
    public bool IsGoal => _isGoal;
    
    
    protected void Move()
    {
        _rigidbody.isKinematic = false;
        _move?.Dispose();
        _move = this.FixedUpdateAsObservable()
            .Subscribe(_ => _rigidbody.AddForce(_speed * transform.right));
    }
     
    protected void Stop()
    {
        _move?.Dispose();
        _gasEffect.Stop();
    }
    protected async void Crash()
    {
        _isCrashed = true;
        if(_crashSound != null)
            _crashSound.Play();
        var explosion = Instantiate(_explosionPrefab, this.transform.position, Quaternion.identity);
        explosion.Play();
        _move?.Dispose();
        _gasEffect.Stop();
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken:this.GetCancellationTokenOnDestroy());
        await this.transform.DOScale(Vector3.zero, 0.5f).ToUniTask(cancellationToken:this.GetCancellationTokenOnDestroy());
        if(this != null)
            Destroy(this.gameObject);
    }

    protected async void PlayHornSound()
    {
        if(_hornSound == null)
            return;
        _hornSound.Play();
        await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken:this.GetCancellationTokenOnDestroy());
        _hornSound.Play();
    }

    protected void OnDestroy()
    {
        _move?.Dispose();
    }
    
}
