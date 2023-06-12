using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshObstacle))]
[RequireComponent(typeof(NavMeshAgent))]

public class NavigationEnemy : MonoBehaviour
{
    [SerializeField]
    private float _distanceAttack;

    private NavMeshAgent _agent;
    private NavMeshObstacle _obstacle;

    private Transform _transform;

    private Damageable
        _target,
        _frontier,
        _player;

    private Vector3 _destinationCache;

    private bool
        _isHuntPlayer,
        _isMoving;

    private float
        _frontierDistance,
        _playerDistance,
        _targetDistance,
        _changingPositionTarget;

    public event Action
        OnReachedPoint,
        OnChangeDestination;

    public Vector3 TargetPosition
    { get; private set; }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _transform = transform;
        _obstacle = GetComponent<NavMeshObstacle>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_target != null)
        {
            Move();
        }
    }

    private void Move()
    {
        TargetPosition = _target.GetAttackPosition(_transform);
        _targetDistance = GetDistance();

        if (_isHuntPlayer)
        {
            ChooseNearTarget();
        }

        CheckChangeDestination();

        if (_targetDistance != 0)
        {
            CheckIsEndPath();
        }
    }

    private void Stop()
    {
        if (_isMoving)
        {
            SwitchAgent(false);
        }
    }

    private float GetDistance()
    {
        _frontierDistance =
            (_transform.position - _frontier.GetAttackPosition(_transform)).sqrMagnitude;

        if (_target == _frontier)
        {
            return _frontierDistance;
        }
        else
        {
            return _playerDistance;
        }
    }

    private void ChooseNearTarget()
    {
        _playerDistance =
            (_transform.position - _player.GetAttackPosition(_transform)).sqrMagnitude;

        _target =
            (_playerDistance < _frontierDistance) ? _player : _frontier;
    }

    private void CheckChangeDestination()
    {
        _changingPositionTarget =
            (_destinationCache - TargetPosition).sqrMagnitude;

        if (_targetDistance > _distanceAttack * _distanceAttack && _changingPositionTarget > 2)
        {
            OnChangeDestination?.Invoke();
            SetDestination(TargetPosition);
        }
    }

    private void CheckIsEndPath()
    {
        if (_targetDistance < _distanceAttack * _distanceAttack)
        {
            OnReachedPoint?.Invoke();
        }
        else
        {
            _isMoving = true;
        }
    }

    private void SetDestination(Vector3 newDestination)
    {
        SwitchAgent(true);
        _agent.destination = newDestination;
        _destinationCache = newDestination;
    }

    private void SwitchAgent(bool enable)
    {
        if (enable)
        {
            _obstacle.enabled = false;
            _agent.enabled = true;
        }
        else
        {
            _agent.enabled = false;
            _obstacle.enabled = true;
        }
    }

    private void OnEnable()
    {
        OnReachedPoint += Stop;
    }

    private void OnDisable()
    {
        OnReachedPoint -= Stop;
        _agent.enabled = false;
        _obstacle.enabled = false;
    }

    public void Construct(Damageable player)
    {
        _player = player;
    }

    public void StartHuntPlayer()
    {
        _isHuntPlayer = true;
    }

    public void SetTarget(Damageable frontier)
    {
        _frontier = frontier;
    }

    public void SetTarget(Damageable frontier, bool isBrokenBarrier)
    {
        if (isBrokenBarrier)
        {
            StartHuntPlayer();
        }

        SetTarget(frontier);
        _target = frontier;

        SetDestination(_target.GetAttackPosition(_transform));
    }
}