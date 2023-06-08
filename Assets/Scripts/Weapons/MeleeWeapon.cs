using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField]
    private MeleeDamage _meleeDamage;

    protected virtual void OnEnable()
    {
        OnStopAttack += _meleeDamage.DamageOff;
    }

    protected virtual void OnDisable()
    {
        OnStopAttack -= _meleeDamage.DamageOff;
    }

    public override void Attack()
    {
        base.Attack();
        _meleeDamage.DamageOn();
    }
}