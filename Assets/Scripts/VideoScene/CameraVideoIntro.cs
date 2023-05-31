using DG.Tweening;
using UnityEngine;

public class CameraVideoIntro : MonoBehaviour
{
    [SerializeField] 
    private Transform _waypoint;
    private Transform _transform;

    private Tweener
        _looking, _moving;

    private void Awake()
    {
        _transform = transform;
    }

    public void Move()
    {
        _moving = _transform.DOMove
            (_waypoint.position, 6).SetEase(Ease.Linear);
    }

    private void OnDisable()
    {
        _looking.Kill();
        _moving.Kill();
    }

    public Tweener Look(Transform target, float speed)
    {
        _looking.Kill();

       return _looking = _transform.DOLookAt
            (target.position, speed).SetSpeedBased();
    }
}
