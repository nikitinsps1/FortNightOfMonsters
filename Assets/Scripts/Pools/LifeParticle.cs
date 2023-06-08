using UnityEngine;

public class LifeParticle : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private GameObject _gameObject;

    private void Awake()
    {
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _gameObject = gameObject;
    }
    private void Update()
    {
        if (_particleSystem.isStopped)
        {
            _gameObject.SetActive(false);
        }
    }
}