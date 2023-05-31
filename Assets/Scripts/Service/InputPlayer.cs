using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class InputPlayer : MonoBehaviour
{
    private Player _player;
    private Camera _camera;
    private Vector3 
        _moveDirection,
        _mousePosition;

    private float _angleRotation;

    private Vector2 _moveInput;
    private Ray _mouseRay;


    [Inject]
    private void Construct(Player player)
    {
        _player = player;
    }

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        

        if (_player.ThisDamageable.isActiveAndEnabled)
        {
        
            Move();
            Attack();
            Rotate();
        }
    }


    private void FixedUpdate()
    {
        if (_player.ThisDamageable.isActiveAndEnabled)
        {
            Move();
        }
    }

    private void Move()
    {
        _moveInput.Set
            (Input.GetAxisRaw(ContainerStrings.Horizontal)
            ,Input.GetAxisRaw(ContainerStrings.Vertical));

        _moveDirection
            .Set(_moveInput.x, 0, _moveInput.y);

        _player
            .Move(_moveDirection.normalized);
    }

    private void Rotate()
    {
        _mouseRay =
            _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_mouseRay, out RaycastHit hit))
        {
            _mousePosition =
                hit.point - _player.ThisTransform.position;

            _mousePosition.Normalize();

            _angleRotation =
                Mathf.Atan2(_mousePosition.x, _mousePosition.z) * Mathf.Rad2Deg;

            _player.Aim(_angleRotation);
        }
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
               _player.Attack();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
           
            _player.StopAttack();
        }
    }
}