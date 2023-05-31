using System;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private TypeSound
        _damageSound, 
        _deadSound;

    [SerializeField]
    private TypeRealations _relation;

    [SerializeField]
    private TypeDeadPartiecles _deadParticles;

    [SerializeField]
    private Collider _collider;

    [SerializeField]
    private ParticleSystem _hitParticle;

    [SerializeField]
    private float
        _health,
        _deadVolume,
        _damageVolume;


    private DeadParticlesConteiner _particlePools;
    private AudioContainer _audio;
    private GameObject _gameObject;

    private float _defaultHealth;

    public float Health => _health;
    public TypeRealations Relation => _relation;

    public Transform ThisTransform 
    { get; private set; }   
    public bool IsAlive 
    { get; private set; }

    public event Action
        OnApplyDamage,
        OnSetHealth,
        OnDead;

    public void Construct(Transform thisTransform, DeadParticlesConteiner particlesPools, AudioContainer audioEffects)
    {


        ThisTransform = thisTransform;
        _gameObject = gameObject;
        _particlePools = particlesPools;
        _audio = audioEffects;
 
    }
    private void Awake()
    {
        _defaultHealth = _health;
    }
    private void OnEnable()
    {
        IsAlive = true;
        _collider.enabled = true;
        _health = _defaultHealth;
    }

    private void Dead()
    {
        OnDead?.Invoke();

        _particlePools.GetObject
            (((int)_deadParticles), ThisTransform.position, ThisTransform.rotation);

        _audio.PlaySound(_deadSound, _deadVolume);

        IsAlive = false;
        _collider.enabled = false;

        _gameObject.SetActive(false);
    }

    public Vector3 GetAttackPosition(Transform attacking)
    {
        return _collider
            .ClosestPoint(attacking.position);
    }

    public virtual void ApplyDamage(float damage)
    {

        OnApplyDamage?.Invoke();
        _audio.PlaySound(_damageSound, _damageVolume);
        _hitParticle.Play();
        _health -= damage;

        if (_health < 0)
        {
    
            Dead();
        }
    }

    public virtual void SetHealth(float value)
    {
        Debug.Log(1);
        _health = value;
        _defaultHealth = value;
        OnSetHealth?.Invoke();
    }
}