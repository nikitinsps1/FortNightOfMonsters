
public class Kamikaze : EnemyFire
{
    public override void Attack()
    {
        Shoot();
        ThisDamageable.ApplyDamage(ThisDamageable.Health+1);
    }
}