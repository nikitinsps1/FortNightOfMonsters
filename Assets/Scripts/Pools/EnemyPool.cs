using Zenject;

public class EnemyPool : Pool
{
    private PlayerHeroLogic _player;
    private BulletsContainer _bullets;
    private ParticlesContainer _particles;
    private AudioContainer _audio;

    [Inject]
    private void Construct(
        BulletsContainer bullets,
        ParticlesContainer particles,
        PlayerHeroLogic player,
        AudioContainer audio)
    {
        _bullets = bullets;
        _particles = particles;
        _audio = audio;
        _player = player;
    }

    public override ObjectPool CreateObject()
    {
        ObjectPool newObject = base.CreateObject();

        EnemyLogic enemy = newObject.GetComponent<EnemyLogic>();


        if (enemy.TryGetComponent(out EnemyRangeLogic enemyFire))
        {
            enemyFire.Construct(
                _player,
                _audio,
                _particles,
                _bullets);
        }
        else
        {
            enemy.Construct(
                _player,
                _audio,
                _particles);
        }

        enemy.ThisDamageable.Construct(_particles, _audio);

        return newObject;
    }
}