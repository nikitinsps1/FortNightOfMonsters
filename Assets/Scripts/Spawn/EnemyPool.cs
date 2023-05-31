using Zenject;

public class EnemyPool : Pool
{
    private Player _player;
    private BulletsConteiner _bullets;
    private DeadParticlesConteiner _particles;
    private AudioContainer _audio;

    [Inject]
    private void Construct
        (BulletsConteiner bullets, DeadParticlesConteiner particles, Player player, AudioContainer audio)
    {
        _bullets = bullets;
        _particles = particles;
        _audio = audio;
        _player = player;
    }

    public override ObjectPool CreateObject()
    {
        ObjectPool newObject
            = base.CreateObject();

        Enemy enemy
            = newObject.GetComponent<Enemy>();


        if (enemy.TryGetComponent(out EnemyFire enemyFire))
        {
            enemyFire
                .Construct(_player, _bullets,_audio, _particles);
        }
        else
        {
            enemy.Construct(_player, _audio, _particles);
        }

        enemy.ThisDamageable
            .Construct(enemy.ThisTransform, _particles, _audio);

        return newObject;
    }
}