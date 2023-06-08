using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] 
    private ParticleSystem _particleSystem;

    [SerializeField] 
    private MeleeDamage _damager;

    [SerializeField] 
    private float _timer;

    private WaitForSeconds _timerCoroutine;

    private void Awake()
    {
        _timerCoroutine = new WaitForSeconds(_timer);
    }

    private IEnumerator Explodes()
    {
        _particleSystem.Play();
        _damager.DamageOn();
        yield return _timerCoroutine;
        _damager.DamageOff();
        yield return new WaitUntil(() => _particleSystem.isPlaying == false);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(Explodes());
    }
}