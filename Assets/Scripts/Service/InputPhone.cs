using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class InputPhone : MonoBehaviour
{
 

    [SerializeField]
    private FixedJoystick
        _walkJoystick,
        _attackJoystick;

    private Player _player;

    private Vector3
        _moveDirection,
        _inputAttackJoystick;

    private Vector2 _inputMoveJoystick;



    private Quaternion _angleCamera;

    [Inject]
    private void Construct(Player player)
    {
        _player = player;
    }

    private void Awake()
    {

        float cameraDegreeY = Camera.main.transform.rotation.eulerAngles.y;

        _angleCamera = Quaternion.Euler(0, cameraDegreeY, 0);


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
        _inputMoveJoystick.
            Set(_walkJoystick.Horizontal, _walkJoystick.Vertical);

        _inputMoveJoystick = _angleCamera * _inputMoveJoystick;

        _inputAttackJoystick
            .Set(_attackJoystick.Horizontal, 0, _attackJoystick.Vertical);

        _inputAttackJoystick = _angleCamera * _inputAttackJoystick;

        _moveDirection
            .Set(_walkJoystick.Horizontal, 0, _walkJoystick.Vertical);

        _moveDirection.Normalize();

       
            _player.Move(_moveDirection);
        


        if (_inputAttackJoystick != Vector3.zero)
        {
           
            _player
                .RotateTarget(_inputAttackJoystick * 100);

            _player.Attack();
        }
        else
        {

            if (_inputMoveJoystick != Vector2.zero)
            {
                _player
                    .RotateTarget(_moveDirection * 100);
            }
            _player.StopAttack();
        }
    }
}