using UnityEngine;
using Zenject;

public class Waypoint : MonoBehaviour
{

    private AudioContainer _audio;

    private Tutorial _tutorial;

    [Inject]
    private void Construct(Tutorial tutorial, AudioContainer audioEffects)
    {
        _tutorial = tutorial;
        _audio = audioEffects;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            _tutorial.ClickCounter();
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        _audio.PlaySound(TypeSound.Victory, 0.15f);
    }
}
