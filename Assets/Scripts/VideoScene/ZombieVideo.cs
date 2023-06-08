using DG.Tweening;
using System;
using UnityEngine;

public class ZombieVideo : MonoBehaviour
{
    [SerializeField]
    private Transform
        _firstPoint,
        _endPoint;

    [SerializeField]
    private ParticleSystem _particleSystem;

    [SerializeField]
    private Renderer _renderer;

    [SerializeField]
    private float _timeReachedPoints;

    private Transform _transform;
    private Sequence _moving;

    public event Action
        OnDead,
        OnReachedFirstPoint;
  
    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        InitWay();
    }

    private void InitWay()
    {
        _moving = DOTween.Sequence();

        _moving.Append(_transform.DOMove
            (_firstPoint.position, _timeReachedPoints)
            .SetEase(Ease.Linear));

        _moving.AppendCallback(OnReachedFirstPoint.Invoke);
        
        _moving.Append(_transform.DOMove
            (_endPoint.position, _timeReachedPoints)
            .SetEase(Ease.Linear));
    }

    private void Dead()
    {
        _renderer.enabled = false;
        _particleSystem.Play();
        OnDead.Invoke();
    }

    private void OnDisable()
    {
        _moving.Kill();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CarVideo>())
        {
            Dead();
        }
    }
}