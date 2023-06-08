using UnityEngine;

[RequireComponent(typeof(NavigationEnemy))]

public abstract class Enemy : Character
{
    private NavigationEnemy _navigation;

    private Damageable _target;
    private AssaultMediator _mediator;

    private bool _isAssault = false;
    private bool _isAttack = false;

    private void Update()
    {
        if (_isAssault)
        {
            Assault();
        }
    }

    private void Assault()
    {
        if (_target.IsAlive == false)
        {
            _target = _mediator.GetTarget(TypeRealations.Friend);
            _navigation.SetTarget(_target);
        }
    }

    private void Dead()
    {
        _mediator.RemoveTarget(ThisDamageable);
        _isAssault = false;
        _isAttack = false;
    }

    protected override void Init()
    {
        base.Init();
        _navigation = GetComponent<NavigationEnemy>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        ThisDamageable.OnDead += Dead;
        _navigation.OnReachedPoint += Attack;
        _navigation.OnChangeDestination += StopAttack;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        ThisDamageable.OnDead -= Dead;
        _navigation.OnReachedPoint -= Attack;
        _navigation.OnChangeDestination -= StopAttack;

        if (_mediator != null)
        {
            _mediator.OnBrokenBarrier -= _navigation.StartHuntPlayer;
        }
    }

    public void StartAssault(AssaultMediator mediator, bool BarrierIsBroken)
    {
        _mediator = mediator;
        _isAssault = true;

        _target = _mediator
            .GetTarget(TypeRealations.Friend);

        _navigation
            .SetTarget(_target, BarrierIsBroken);

        _mediator.OnBrokenBarrier += _navigation.StartHuntPlayer;
    }

    public virtual void Construct(Player player, AudioContainer audio, ParticlesContainer particles)
    {
        if (ThisDamageable == null)
        {
            ThisDamageable = GetComponent<Damageable>();
        }

        ThisDamageable
               .Construct(particles, audio);

        _navigation
            .Construct(player.ThisDamageable);
    }

    public override void Attack()
    {
        if (_isAttack == false)
        {
            _isAttack = true;
            base.Attack();
        }

        RotateTarget(_navigation._targetPosition);
    }

    public override void StopAttack()
    {
        if (_isAttack)
        {
            _isAttack = false;
            base.StopAttack();
        }
    }
}