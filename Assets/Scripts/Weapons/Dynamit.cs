using UnityEngine;
using Zenject;

public class Dynamit : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosion;

    private AudioContainer _audioEffects;

    [Inject] 
    private void Construct(AudioContainer audioEffects)
    {
        _audioEffects = audioEffects;
    }

    private void OnDisable()
    {
        if (_explosion != null)
        {
            _audioEffects.PlaySound(TypeSound.Explosion, 0.5f);
            _explosion.SetActive(true);
        }

    }
}