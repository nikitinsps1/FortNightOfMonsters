using UnityEngine;
using Zenject;

public class TutorialObserver : MonoBehaviour
{
    [SerializeField]
    private TutorialStage[] _stages;

    private SceneLoader _sceneChanger;

    private int _stageCounter = 0 ;

    [Inject]
    private void Construct(SceneLoader sceneChanger)
    {
        _sceneChanger = sceneChanger;
    }

    private void OnEnable()
    {
        for (int i = 0; i < _stages.Length; i++)
        {
            _stages[i].OnComplete += ChangeStage;
        }
    }

    private void ChangeStage()
    {
        _stageCounter++;

        if (_stageCounter == _stages.Length)
        {
            _sceneChanger.Change(TypeScene.MainMenu);
        }
        else
        {
            _stages[_stageCounter].StartTask();
        }
    }
}