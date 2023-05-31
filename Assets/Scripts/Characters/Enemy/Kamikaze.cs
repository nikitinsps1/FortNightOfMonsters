using System;

public class Kamikaze : EnemyFire
{
    private Action _suicide;

    protected override void Init()
    {
        base.Init();
        _suicide = delegate { ThisDamageable.ApplyDamage(100); };
    }

    public override void Attack()
    {
        Shoot();
        _suicide.Invoke();
    }
}