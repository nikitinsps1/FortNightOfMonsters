using DG.Tweening;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Damageable))]
public class Barricade : MonoBehaviour
{
    [SerializeField] 
    private AssaultMediator _assaultMediator;

    [SerializeField]
    private Transform _transformMesh;

    [SerializeField]
    private GameObject _dynamiteExplosion;

    private AudioContainer _audioEffects;
    private ParticlesContainer _deadParticles;
    private SaveData _saveData;

    public Damageable ThisDamageable
    { get; private set; }


    [Inject]
    private void Construct(AudioContainer audio, ParticlesContainer deadParticles, SaveData save)
    {
        _audioEffects = audio;
        _deadParticles = deadParticles;
        _saveData = save;
    }

    private void Awake()
    {
        ThisDamageable = GetComponent<Damageable>();
        ThisDamageable.Construct(_deadParticles, _audioEffects);
    }

    private void OnDead()
    {
        int levelUpgradeDynamite = _saveData.Fort
            .ThisDictionary[(int)TypeFortUpgrade.Dynamite];

        if (levelUpgradeDynamite>0)
        {
            _audioEffects.PlaySound(TypeSound.Explosion,0.1f);
            _dynamiteExplosion.gameObject.SetActive(true);
        }

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
}