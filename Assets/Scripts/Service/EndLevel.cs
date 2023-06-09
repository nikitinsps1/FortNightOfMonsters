using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EndLevel : MonoBehaviour
{
    [SerializeField]
    private EndLevelPanel
        _victory,
        _falls,
        _final;

    private Dictionary<int, EndLevelPanel> _panels;

    private SceneChanger _levelChanger;

    [Inject]
    private void Construct(SceneChanger levelChanger)
    {
        _levelChanger = levelChanger;
    }

    private void Awake()
    {
        _panels = new Dictionary<int, EndLevelPanel>()
        {
            {(int)TypeEndLevel.Victory, _victory},
            {(int)TypeEndLevel.Fail, _falls},
            {(int)TypeEndLevel.GameComplete, _final},
        };
    }

    public void OnEnd(TypeEndLevel typeEnd, string endText)
    {
        EndLevelPanel currentPanel = _panels[(int)typeEnd];
        currentPanel.gameObject.SetActive(true);

        TypeScene nextScene;

        if (typeEnd == TypeEndLevel.Victory || typeEnd == TypeEndLevel.Fail)
        {
            nextScene = TypeScene.Fort;
        }
        else
        {
            nextScene = TypeScene.MainMenu;
        }

        Action action = delegate
        {
            _levelChanger.Change(nextScene);
        };

        currentPanel.Init(action, endText);
    }
}