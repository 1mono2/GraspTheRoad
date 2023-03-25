using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class CarGenerator : MonoBehaviour
{
    [SerializeField] private AbstractCarBehavior _carPrefab;
    [SerializeField] private GameObject _startPoint;

    [SerializeField] private float _distanceThreshold = 2f;
    [SerializeField] private float _generateInterval = 1f;
    [SerializeField] private ParticleSystem _cloudPrefab;
    private AbstractCarBehavior _currentCarPrefab;
    
    
    void Start()
    {   
        //GenerateCar();

        this.UpdateAsObservable()
            .ThrottleFirst(TimeSpan.FromSeconds(_generateInterval))
            .Subscribe(_ =>
            {
                if (_currentCarPrefab == null)
                {
                    GenerateCar();
                    return;
                }
                if (CheckDistanceOver(_distanceThreshold, _currentCarPrefab.transform.position, _startPoint.transform.position))
                {
                    GenerateCar();
                }
            });
    }
    
    private void GenerateCar()
    {
        var cloud = Instantiate(_cloudPrefab, _startPoint.transform.position + new Vector3(0, 1, -1), Quaternion.identity);
        cloud.Play();
        _currentCarPrefab = Instantiate(_carPrefab, _startPoint.transform.position, Quaternion.identity);
        var carRigidbody = _currentCarPrefab.GetComponent<Rigidbody2D>();
        carRigidbody.bodyType = RigidbodyType2D.Dynamic;
    }

    private static bool CheckDistanceOver(float threshold, Vector3 position1, Vector3 position2)
    {
        if (Vector3.Distance(position1, position2) > threshold)
        {
            return true;
        }
        return false;
    }
    

}
