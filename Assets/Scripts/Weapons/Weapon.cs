using UnityEngine;
using Zenject;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    private TypeWeapons _type;

    [SerializeField]
    private TypeSound _typeSound;

    protected AudioContainer _audioEffect;

    public TypeWeapons Type => _type;

    [Inject]
    private void Construct(AudioContainer audioEffects)
    {
        _audioEffect = audioEffects;
    }

    protected virtual void OnSound()
    {
        _audioEffect.PlaySound(_typeSound, 0.15f);
    }

    public virtual void Attack()
    {
        OnSound();
    }

    public abstract void StopAttack();
}