using DG.Tweening;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Damageable))]
public class MainHouse : MonoBehaviour
{
    [SerializeField]
    private GameObject
        _liveHouse,
        _defenseBugs;

    [SerializeField] 
    private GameObject[] _dinamites;

    [SerializeField]
    private Transform _transformMesh;


    private AudioContainer _audio;
    private DeadParticlesConteiner _particles;
    public Damageable ThisDamageable
    { get; private set; }


    [Inject]
    private void Construct(AudioContainer audioEffects, DeadParticlesConteiner particleSystem)
    {
        _audio = audioEffects;
        _particles = particleSystem;
    }

    private void Awake()
    {
        ThisDamageable = GetComponent<Damageable>();
        ThisDamageable.Construct(_transformMesh, _particles, _audio);
    }


    private void OnEnable()
    {
        ThisDamageable.OnApplyDamage += OnApplyDamage;
    }

    private void OnDisable()
    {
        ThisDamageable.OnApplyDamage -= OnApplyDamage;
    }

    private void OnApplyDamage()
    {
       _transformMesh.DOShakePosition(0.5f, 0.05f);
    }

    public void Upgrade(TypeUpgradesBuildings type)
    {
        switch (type)
        {
            case TypeUpgradesBuildings.MainHouseDefense:
                _defenseBugs.SetActive(true);
                ThisDamageable.SetHealth(50);
                break;

            case TypeUpgradesBuildings.LiveHouse:
                _liveHouse.SetActive(true);
                break;

            case TypeUpgradesBuildings.Dynamite:
                for (int i = 0; i < _dinamites.Length; i++)
                {
                    _dinamites[i].SetActive(true);
                }
                break;

            default:
                break;
        }
    }
}