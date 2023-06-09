using UnityEngine;
using Zenject;

public class Waypoint : MonoBehaviour
{
    [SerializeField]
    private TutorialStage _tutorialStage;

    private AudioContainer _audio;


    [Inject]
    private void Construct(AudioContainer audio)
    {
        _audio = audio;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerHeroLogic>())
        {
            _tutorialStage.ClickCounter();
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        _audio.PlaySound(TypeSound.Victory, 0.05f);
    }
}
