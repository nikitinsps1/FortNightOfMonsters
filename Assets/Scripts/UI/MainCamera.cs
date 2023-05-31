using DG.Tweening;
using UnityEngine;
using Zenject;

public class MainCamera : MonoBehaviour
{
    [SerializeField]
    private float
        _smoothing,
        _postionY,
        _offsetZ,
        _offsetX;

    private Player _player;
    private Transform _transfrom;

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
        _transfrom = transform;
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
        _transfrom.position = new Vector3
            (_player.ThisTransform.position.x + _offsetX
            ,_postionY
            ,_player.ThisTransform.position.z + _offsetZ);

        _offset = _transfrom.position - _player.ThisTransform.position;
    }

    private void FixedUpdate()
    {
        if (_player != null)
        {
            _newPosition =
                _player.ThisTransform.position + _offset;

            _transfrom.position =
                Vector3.Lerp
                (_transfrom.position, _newPosition, _smoothing * Time.fixedDeltaTime);
        }
    }

    public void Shake()
    {
        _shake = _transfrom?.DOShakeRotation(0.2f, 0.5f);
    }
}
