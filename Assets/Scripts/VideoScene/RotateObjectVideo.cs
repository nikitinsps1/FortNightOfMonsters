using DG.Tweening;
using UnityEngine;

public class RotateObjectVideo : MonoBehaviour
{
    [SerializeField]
    private Transform _transform;

    [SerializeField]
    private float _speed;


    private Tween _rotating;


    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        _rotating = _transform.DORotate(new Vector3(360, 0, 0),
            _speed, RotateMode.LocalAxisAdd)
            .SetSpeedBased()
            .SetEase(Ease.Linear)
            .SetLoops(-1);
     
    }

    private void OnDisable()
    {
        _rotating.Kill();
    }
}