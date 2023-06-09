using UnityEngine;
using Zenject;

public class InputPhone : MonoBehaviour
{
    [SerializeField]
    private Joystick
        _walkJoystick,
        _attackJoystick;

    private PlayerHeroLogic _player;

    private Vector3
        _inputAttackJoystick,
        _inputMoveJoystick,
        _rotateVector;

    private Quaternion _angleCamera;

    [Inject]
    private void Construct(PlayerHeroLogic player)
    {
        _player = player;
    }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        float cameraDegreeY =
            Camera.main.transform.rotation.eulerAngles.y;

        _angleCamera =
            Quaternion.Euler(0, cameraDegreeY, 0);
    }

    private void FixedUpdate()
    {
        if (_player.isActiveAndEnabled)
        {
            Move();
        }
    }

    private void Move()
    {
        SetInputVector(ref _inputMoveJoystick, _walkJoystick);
        SetInputVector(ref _inputAttackJoystick, _attackJoystick);

        _inputMoveJoystick.Normalize();
        _player.Move(_inputMoveJoystick);

        if (_inputAttackJoystick != Vector3.zero)
        {
            _rotateVector = _inputAttackJoystick;
            _player.Attack();
        }
        else
        {
            _rotateVector = _inputMoveJoystick;
            _player.StopAttack();
        }

        if (_rotateVector != Vector3.zero)
            _player.RotateTarget(_rotateVector * _player.SpeedRotation);

    }

    private void SetInputVector(ref Vector3 vector, Joystick joystick)
    {
        vector.Set(joystick.Horizontal, 0, joystick.Vertical);
        vector = _angleCamera * vector;
    }
}