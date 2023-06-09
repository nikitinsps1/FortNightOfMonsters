using DG.Tweening;
using UnityEngine;

public class CameraVideoIntro : MonoBehaviour
{
    [SerializeField]
    private Transform _waypoint;
    private Transform _transform;

    private Tweener
        _looking,
        _moving;

    private float _moveDuration = 6;

    private void Awake()
    {
        _transform = transform;
    }

    public void Move()
    {
        _moving =
            _transform.DOMove(_waypoint.position, _moveDuration)
            .SetEase(Ease.Linear);
    }

    private void OnDisable()
    {
        _looking.Kill();
        _moving.Kill();
    }

    public Tweener Look(Transform target, float speed)
    {
        _looking.Kill();

        return _looking =
             _transform.DOLookAt(target.position, speed)
             .SetSpeedBased();
    }
}