using UnityEngine;

public class EnemyMele : Enemy
{
    [SerializeField]
    private MelleDamage _meleeDamage;

    public override void StopAttack()
    {
        base.StopAttack();
        _meleeDamage.DamageOff();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        ThisCharacterAnimator
            .OnAnimationEvent += _meleeDamage.DamageOn;

        ThisCharacterAnimator
            .OnEndAnimationEvent += _meleeDamage.DamageOff;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        ThisCharacterAnimator
            .OnAnimationEvent -= _meleeDamage.DamageOn;

        ThisCharacterAnimator
            .OnEndAnimationEvent -= _meleeDamage.DamageOff;
    }
}