using System;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField]
    private TypeSound
        _damageSound,
        _deadSound;

    [SerializeField]
    private TypeRelations _relation;

    [SerializeField]
    private TypeDeadParticles _deadParticles;

    [SerializeField]
    private Collider _collider;

    [SerializeField]
    private ParticleSystem _hitParticle;

    [SerializeField]
    private float
        _health,
        _deadVolume,
        _damageVolume;

    private ParticlesContainer _particlePools;
    private AudioContainer _audio;
    private GameObject _gameObject;

    private float _defaultHealth;
    public Transform ThisTransform
    { get; private set; }
    public bool IsAlive
    { get; private set; }

    public event Action
        OnApplyDamage,
        OnSetHealth,
        OnDead;

    public TypeRelations Relation => _relation;
    public float Health => _health;

    public void Construct(ParticlesContainer particles, AudioContainer audio)
    {
        _particlePools = particles;
        _audio = audio;
    }

    private void Awake()
    {
        ThisTransform = transform;

        _defaultHealth = _health;
        _gameObject = gameObject;
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

        _particlePools.GetObject(
            (int)_deadParticles,
            ThisTransform.position,
            ThisTransform.rotation);

        _audio.PlaySound(_deadSound, _deadVolume);

        IsAlive = false;
        _collider.enabled = false;

        _gameObject.SetActive(false);
    }

    public Vector3 GetAttackPosition(Transform attacking)
    {
        return _collider.ClosestPoint(attacking.position);
    }

    public void ApplyDamage(float damage)
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

    public void SetHealth(float value)
    {
        _health = value;
        _defaultHealth = value;
        OnSetHealth?.Invoke();
    }
}