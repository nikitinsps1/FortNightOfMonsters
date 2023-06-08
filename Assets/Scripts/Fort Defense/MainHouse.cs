using DG.Tweening;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Damageable))]
public class MainHouse : MonoBehaviour
{
    [SerializeField]
    private GameObject
        _liveHouse,
        _defenseBags;

    [SerializeField] 
    private GameObject[] _dynamites;

    [SerializeField]
    private Transform _transformMesh;

    private AudioContainer _audio;
    private ParticlesContainer _particles;

    public Damageable ThisDamageable
    { get; private set; }

    [Inject]
    private void Construct(AudioContainer audio, ParticlesContainer particles)
    {
        _audio = audio;
        _particles = particles;
    }

    private void Awake()
    {
        ThisDamageable = GetComponent<Damageable>();
        ThisDamageable.Construct(_particles, _audio);
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
                _defenseBags.SetActive(true);
                ThisDamageable.SetHealth(50);
                break;

            case TypeUpgradesBuildings.LiveHouse:
                _liveHouse.SetActive(true);
                break;

            case TypeUpgradesBuildings.Dynamite:
                for (int i = 0; i < _dynamites.Length; i++)
                {
                    _dynamites[i].SetActive(true);
                }
                break;

            default:
                break;
        }
    }
}