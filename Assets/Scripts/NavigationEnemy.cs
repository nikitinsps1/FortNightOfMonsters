using System;
using UnityEngine;
using UnityEngine.AI;

public class NavigationEnemy : MonoBehaviour
{
    [SerializeField]
    private float _distanceForAttack;

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private NavMeshObstacle _obstacle;

    private Transform _transform;

    private Damageable
        _target,
        _frontier,
        _player;

    private Vector3 _destionationCache;

    private bool _isHuntPlayer;
    private bool _isMoving = false;

    private float
        _frontierDistance,
        _playerDistance,
        _targetDistance,
        _changingPositionTarget;

    public event Action 
        OnReachedPoint,
        OnChangeDestination;

    public Vector3 _targetPosition
    { get; private set; }

    public void Init(Transform thisTransform, Damageable player)
    {
        _transform = thisTransform;
        _player = player;
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
        _targetPosition = _target.GetAttackPosition(_transform);
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

        _target = (_playerDistance < _frontierDistance) ? _player : _frontier;

    }

    private void CheckChangeDestination()
    {
        _changingPositionTarget =
            (_destionationCache - _targetPosition).sqrMagnitude;

        if (_targetDistance > _distanceForAttack * _distanceForAttack && _changingPositionTarget > 2)
        {
            OnChangeDestination?.Invoke();
            SetDestination(_targetPosition);
        }
    }

    private void CheckIsEndPath()
    {
        if (_targetDistance < _distanceForAttack * _distanceForAttack)
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
        _destionationCache = newDestination;
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