using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class InputPlayer : MonoBehaviour
{
    private PlayerHeroLogic _player;
    private Camera _camera;

    private Vector3 _moveDirection;
    private Vector2 _moveInput;
    private Ray _mouseRay;

    private bool _isMouseOnUI;

    [Inject]
    private void Construct(PlayerHeroLogic player)
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
        _moveInput.Set(
            Input.GetAxisRaw(ContainerStrings.Horizontal),
            Input.GetAxisRaw(ContainerStrings.Vertical));

        _moveDirection.
            Set(_moveInput.x, 0, _moveInput.y);

        _player.Move
           (_moveDirection.normalized);
    }

    private void Rotate()
    {
        _mouseRay =
            _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_mouseRay, out RaycastHit hit))
        {
            _player.RotateTarget(hit.point);
        }
    }

    private void Attack()
    {
        _isMouseOnUI =
            EventSystem.current.IsPointerOverGameObject();

        if (Input.GetMouseButtonDown(0) && _isMouseOnUI == false)
            _player.Attack();

        if (Input.GetMouseButtonUp(0))
            _player.StopAttack();
    }
}