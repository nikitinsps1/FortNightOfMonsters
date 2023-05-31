using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Arsenal))]
public class Guard : Character
{
    [SerializeField]
    private AssaultMediator _assaultMediator;

    private AudioContainer _audioEffects;
    private DeadParticlesConteiner _deadParticles;
    private Damageable _target;
    private WaitForSeconds _delayShootAnimation;

    private bool _isAssault;
    public Arsenal ThisArsenal
    { get; private set; }

    [Inject]
    private void Construct(AudioContainer effects, DeadParticlesConteiner deadParticles)
    {
        _deadParticles = deadParticles;
        _audioEffects = effects;
    }


    private void Update()
    {
        if (_isAssault)
        {
            Defend();
        }
    }

    private void Defend()
    {
   

        if (_target == null || _target.IsAlive == false)
        {
            Damageable newTarget =
                _assaultMediator.GetTarget(TypeRealations.Enemy);

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
            RotateTarget
                (_target.ThisTransform.position);
        }
    }

    private void OnDead()
    {
        _assaultMediator.RemoveTarget(ThisDamageable);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        ThisArsenal
            .OnChangedWeapon += ThisCharacterAnimator.ChangeWeapon;

        ThisDamageable
            .OnDead += OnDead;

        ThisCharacterAnimator
            .OnAnimationEvent += ThisArsenal.Attack;

        ThisCharacterAnimator
            .OnEndAnimationEvent += ThisArsenal.StopAttack;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        ThisArsenal
            .OnChangedWeapon -= ThisCharacterAnimator.ChangeWeapon;

        ThisDamageable
            .OnDead -= OnDead;

        ThisCharacterAnimator
            .OnAnimationEvent -= ThisArsenal.Attack;

        ThisCharacterAnimator
            .OnEndAnimationEvent -= ThisArsenal.StopAttack;
    }

    protected override void Init()
    {
        base.Init();

        ThisDamageable
            .Construct(ThisTransform, _deadParticles, _audioEffects);

        ThisArsenal = GetComponent<Arsenal>();
        ThisArsenal.Init();

        _delayShootAnimation
            = new WaitForSeconds(Random.value);
    }

    public override void Attack()
    {
        _target
            = _assaultMediator.GetTarget(TypeRealations.Enemy);

        base.Attack();
        _isAssault = true;
    }

    public override void StopAttack()
    {
        base.StopAttack();
        _isAssault = false;
    }

    public IEnumerator StartShooting()
    {
        yield return _delayShootAnimation;
        Attack();
    }
}