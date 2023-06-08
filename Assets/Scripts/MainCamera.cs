using DG.Tweening;
using UnityEngine;
using Zenject;

public class MainCamera : MonoBehaviour
{
    [SerializeField]
    private float
        _smoothing,
        _positionY,
        _offsetZ,
        _offsetX;

    private Player _player;
    private Transform _transform;
    private Tween _shake;

    private Vector3 _offset;
    private Vector3 _newPosition;

    [Inject]
    private void Construct(Player player)
    {
        _player = player;
    }

    private void Awake()
    {
        _transform = transform;
    }

    private void OnEnable()
    {
        _player.ThisCharacterAnimator.OnAnimationEvent += Shake; 
    }

    private void OnDisable()
    {
        _shake.Kill();
        _player.ThisCharacterAnimator.OnAnimationEvent -= Shake;
    }

    private void Start()
    {
        _transform.position = new Vector3
            (_player.ThisTransform.position.x + _offsetX,
            _positionY,
            _player.ThisTransform.position.z + _offsetZ);

        _offset = _transform.position - _player.ThisTransform.position;
    }

    private void FixedUpdate()
    {
        if (_player != null)
        {
            _newPosition =
                _player.ThisTransform.position + _offset;

            _transform.position = Vector3.Lerp
                (_transform.position, _newPosition, _smoothing * Time.fixedDeltaTime);
        }
    }

    public void Shake()
    {
        _shake = _transform?.DOShakeRotation(0.2f, 0.5f);
    }
}
