using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] 
    private ParticleSystem _particleSystem;

    [SerializeField] 
    private MelleDamage _damager;

    [SerializeField] 
    private float _timer;

    private WaitForSeconds _timerCorrutine;
    private void Awake()
    {
        _timerCorrutine = new WaitForSeconds(_timer);
    }

    private IEnumerator Explodes()
    {
        _particleSystem.Play();
        _damager.DamageOn();
        yield return _timerCorrutine;
        _damager.DamageOff();
        yield return new WaitUntil(() => _particleSystem.isPlaying == false);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(Explodes());
    }
}