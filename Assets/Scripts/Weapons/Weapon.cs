using System;
using UnityEngine;
using Zenject;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    private TypeWeapons _type;

    [SerializeField]
    private TypeSound _typeSound;

    protected AudioContainer _audio;
    public event Action OnStopAttack;
    public TypeWeapons Type => _type;

    [Inject]
    private void Construct(AudioContainer audio)
    {
        _audio = audio;
    }

    protected virtual void Sound()
    {
        _audio.PlaySound(_typeSound, 0.15f);
    }

    public virtual void Attack()
    {
        Sound();
    }

    public void StopAttack()
    {
        OnStopAttack?.Invoke();
    }
}