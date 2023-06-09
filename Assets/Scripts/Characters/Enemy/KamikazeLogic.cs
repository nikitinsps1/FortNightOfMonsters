
public class KamikazeLogic : EnemyRangeLogic
{
    public override void Attack()
    {
        Shoot();
        ThisDamageable.ApplyDamage(ThisDamageable.Health + 1);
    }
}