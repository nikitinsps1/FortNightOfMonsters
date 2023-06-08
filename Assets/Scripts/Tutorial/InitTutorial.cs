using UnityEngine;
using Zenject;

public class InitTutorial : MonoBehaviour
{
    [SerializeField]
    private TutorialStage _firstStage;

    private AudioContainer _audio;

    [Inject]
    private void Construct(AudioContainer audio)
    {
        _audio = audio;
    }

    private void Start()
    {
        _audio.PlayMusic(TypeMusic.SciFi);
        _firstStage.StartTask();
    }
}