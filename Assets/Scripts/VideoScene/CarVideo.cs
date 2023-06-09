using DG.Tweening;
using UnityEngine;

public class CarVideo : MonoBehaviour
{
    [SerializeField]
    private Transform
        _body,
        _zombie,
        _endPoint;

    [SerializeField]
    private float
        _timeToZombie,
        _timeToEndPoint;

    private Sequence _moving;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    public void Ride()
    {
        _moving = DOTween.Sequence();

        _moving.Append
            (_transform
            .DOMove(_zombie.position, _timeToZombie)
            .SetEase(Ease.Linear));

        _moving.Append(_transform
            .DOMove(_endPoint.position, _timeToEndPoint)
            .SetEase(Ease.Linear));
    }

    private void OnDisable()
    {
        _moving.Kill();
    }
}
