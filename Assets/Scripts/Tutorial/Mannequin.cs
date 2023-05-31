using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterAnimator))]
[RequireComponent (typeof(Damageable))]
public class Mannequin : MonoBehaviour
{
    private AudioContainer _audio;

    private CharacterAnimator _characterAnimator;
    private Tutorial _tutorial;
    private Transform _transform;
    private Damageable _damagaeble;

    private DeadParticlesConteiner _deadParticlesConteiner;

    [Inject]
    private void Construct(Tutorial tutorial, DeadParticlesConteiner deadParticles, AudioContainer audioEffects)
    {
        _tutorial = tutorial;
        _deadParticlesConteiner = deadParticles;
        _audio = audioEffects;
    }

    private void Awake()
    {
        _characterAnimator = GetComponent<CharacterAnimator>();
        _transform = GetComponent<Transform>();
        _damagaeble = GetComponent<Damageable>();
    }

    private void Start()
    {
        _damagaeble.Construct(_transform, _deadParticlesConteiner, _audio);
    }

    private void OnEnable()
    {
        _damagaeble.OnDead += OnDead;
        _damagaeble.OnApplyDamage += OnApplyDamage;
    }

    private void OnDisable()
    {

        _damagaeble.OnDead -= OnDead;
        _damagaeble.OnApplyDamage -= OnApplyDamage;
    }

    public virtual void OnDead() 
    {
        _tutorial.ClickCounter();
    }

    public void OnApplyDamage()
    {
        _characterAnimator.ApplyHit();
    }
}
