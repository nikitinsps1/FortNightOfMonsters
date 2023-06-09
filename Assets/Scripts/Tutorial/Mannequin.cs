using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterAnimator))]
[RequireComponent(typeof(Damageable))]

public class Mannequin : MonoBehaviour
{
    [SerializeField]
    private TutorialStage _tutorialStage;

    private AudioContainer _audio;
    private CharacterAnimator _characterAnimator;
    private Damageable _damageable;
    private ParticlesContainer _particles;

    [Inject]
    private void Construct(ParticlesContainer particles, AudioContainer audio)
    {
        _particles = particles;
        _audio = audio;
    }

    private void Awake()
    {
        _characterAnimator = GetComponent<CharacterAnimator>();
        _damageable = GetComponent<Damageable>();
    }

    private void Start()
    {
        _damageable.Construct(_particles, _audio);
    }

    private void OnEnable()
    {
        _damageable.OnDead += OnDead;
        _damageable.OnApplyDamage += OnApplyDamage;
    }

    private void OnDisable()
    {
        _damageable.OnDead -= OnDead;
        _damageable.OnApplyDamage -= OnApplyDamage;
    }

    public void OnDead()
    {
        _tutorialStage.ClickCounter();
    }

    public void OnApplyDamage()
    {
        _characterAnimator.ApplyHit();
    }
}
