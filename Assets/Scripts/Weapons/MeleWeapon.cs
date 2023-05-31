using UnityEngine;

public class MeleWeapon : Weapon
{
    [SerializeField]
    private MelleDamage _melleDanage;

    protected virtual void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            StopAttack();
        }
    }

    public override void Attack()
    {
        base.Attack();
        _melleDanage.DamageOn();
    }

    public override void StopAttack()
    {
        _melleDanage.DamageOff();
    }
}