using UnityEngine;

public class FlameBrower : MeleWeapon
{
    [SerializeField]
    private ParticleSystem _attackParticle;


    protected override void OnSound()
    {

        _audioEffect.Flame.Play();

    }

    public override void Attack()
    {
        base.Attack();
        _attackParticle.Play();
    }

    public override void StopAttack()
    {
        base.StopAttack();
        _audioEffect.Flame?.Stop();
        _attackParticle.Stop();
    }
}