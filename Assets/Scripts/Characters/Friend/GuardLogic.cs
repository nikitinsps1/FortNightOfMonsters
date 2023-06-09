using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Arsenal))]

public class GuardLogic : CharacterLogic
{
    [SerializeField]
    private AssaultMediator _assaultMediator;

    private AudioContainer _audioEffects;
    private ParticlesContainer _deadParticles;
    private WaitForSeconds _delayShootAnimation;
    private Damageable _target;

    private bool _isAlarm;
    public Arsenal ThisArsenal
    { get; private set; }

    [Inject]
    private void Construct(AudioContainer audio, ParticlesContainer particles)
    {
        _deadParticles = particles;
        _audioEffects = audio;
    }

    private void Update()
    {
        if (_isAlarm)
        {
            Defend();
        }
    }

    private void Defend()
    {
        if (_target == null || _target.IsAlive == false)
        {
            Damageable newTarget =
                _assaultMediator.GetTarget(TypeRelations.Enemy);

            if (newTarget == null)
            {
                StopAttack();
            }
            else
            {
                _target = newTarget;
            }
        }
        else
        {
            RotateTarget(_target.ThisTransform.position);
        }
    }

    private void OnDead()
    {
        _assaultMediator.RemoveTarget(ThisDamageable);
    }

    protected override void Init()
    {
        base.Init();

        ThisDamageable.Construct( _deadParticles, _audioEffects);

        ThisArsenal = GetComponent<Arsenal>();
        ThisArsenal.Init();
        _delayShootAnimation = new WaitForSeconds(Random.value);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        ThisArsenal.OnChangedWeapon +=
            ThisCharacterAnimator.ChangeWeapon;

        ThisCharacterAnimator.OnAnimationEvent +=
            ThisArsenal.Attack;

        ThisCharacterAnimator.OnEndAnimationEvent +=
            ThisArsenal.StopAttack;

        ThisDamageable.OnDead += OnDead;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        ThisArsenal.OnChangedWeapon -=
            ThisCharacterAnimator.ChangeWeapon;

        ThisCharacterAnimator.OnAnimationEvent -=
            ThisArsenal.Attack;

        ThisCharacterAnimator.OnEndAnimationEvent -=
            ThisArsenal.StopAttack;

        ThisDamageable.OnDead -= OnDead;
    }

    public IEnumerator StartShooting()
    {
        yield return _delayShootAnimation;
        Attack();
    }

    public override void Attack()
    {
        _target = _assaultMediator.GetTarget(TypeRelations.Enemy);

        base.Attack();
        _isAlarm = true;
    }

    public override void StopAttack()
    {
        base.StopAttack();
        _isAlarm = false;
    }
}