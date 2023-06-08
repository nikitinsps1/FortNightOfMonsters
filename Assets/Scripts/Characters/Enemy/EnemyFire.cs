using UnityEngine;

public class EnemyFire : Enemy
{
    [SerializeField]
    private Transform _shootPosition;

    [SerializeField]
    private TypeBullets _typeBullets;

    [SerializeField]
    private TypeSound _shootingSound;

    [SerializeField]
    private float _shootVolume;

    private AudioContainer _audio;
    private BulletsContainer _bullets;

    public void Construct(
        Player player, 
        AudioContainer audio, 
        ParticlesContainer particles, 
        BulletsContainer bullets)
    {
        Construct(player, audio,particles);
        _bullets = bullets;
        _audio = audio;
    }

    protected void Shoot()
    {
        ObjectPool bullet =
            _bullets.GetObject
            ((int)_typeBullets, _shootPosition.position, ThisTransform.rotation);

        _audio.PlaySound(_shootingSound, _shootVolume);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        ThisCharacterAnimator.OnAnimationEvent += Shoot;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        ThisCharacterAnimator.OnAnimationEvent -= Shoot;
    }
}