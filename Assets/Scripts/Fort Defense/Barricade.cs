using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Damageable))]
public class Barricade : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _upgradesMeshes;

    [SerializeField] 
    private AssaultMediator _assaultMediator;

    [SerializeField]
    private Transform _transformMesh;


    private AudioContainer _audioEffects;
    private DeadParticlesConteiner _deadParticles;


    public Damageable ThisDamageable
    { get; private set; }


    [Inject]
    private void Construct(AudioContainer audio, DeadParticlesConteiner deadParticles)
    {
        _audioEffects = audio;
        _deadParticles = deadParticles;
    }

    private void Awake()
    {
        ThisDamageable = GetComponent<Damageable>();
        ThisDamageable.Construct(_transformMesh, _deadParticles, _audioEffects);
    }


    private void OnDead()
    {
        _assaultMediator.BrokeBarrier();
        _assaultMediator.RemoveTarget(ThisDamageable);
    }

    private void OnApplyDamage()
    {
        _transformMesh.DOShakePosition(0.5f, 0.05f);
    }

    private void OnEnable()
    {
        ThisDamageable.OnDead += OnDead;
        ThisDamageable.OnApplyDamage += OnApplyDamage;
    }

    private void OnDisable()
    {
        ThisDamageable.OnDead -= OnDead;
        ThisDamageable.OnApplyDamage -= OnApplyDamage;
    }

    public void Upgrade(int level, float newHealthValue)
    {
        for (int i = 0; i < _upgradesMeshes.Count; i++)
        {
            if (i == level)
            {
                _upgradesMeshes[i].SetActive(true);
            }
            else
            {
                _upgradesMeshes[i].SetActive(false);
            }
        }

        ThisDamageable.SetHealth(newHealthValue);
    }
}