using UnityEngine;

public class FlameThrower : MeleeWeapon
{
    [SerializeField]
    private ParticleSystem _attackParticle;

    protected override void OnEnable()
    {
        base.OnEnable();
        OnStopAttack += _audio.Flame.Stop;
        OnStopAttack += _attackParticle.Stop;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        OnStopAttack -= _audio.Flame.Stop;
        OnStopAttack -= _attackParticle.Stop;
    }


    protected override void Sound()
    {
        _audio.Flame.Play();
    }

    public override void Attack()
    {
        base.Attack();
        _attackParticle.Play();
    }
}