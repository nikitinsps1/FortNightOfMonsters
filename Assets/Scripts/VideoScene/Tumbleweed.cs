using DG.Tweening;
using UnityEngine;

public class Tumbleweed : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField] 
    private float _speed;

    private Tweener _moving;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        Move();
    }

    private void OnDisable()
    {
        _moving.Kill();
    }

    public  void Move()
    {
        _moving = _transform
            .DOMove(_target.position, _speed)
            .SetSpeedBased()
            .SetEase(Ease.Linear);
    }
}
