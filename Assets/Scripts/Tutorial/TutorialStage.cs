using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using YG;
using Zenject;

public  class TutorialStage : MonoBehaviour
{
    [SerializeField, TextArea(1, 20)]
    private string _descriptionDesktop;

    [SerializeField, TextArea(1, 20)]
    private string _descriptionPhone;

    [SerializeField]
    private GameObject[] _activatingObjects;

    [SerializeField]
    private UnityEvent _additionalAction;

    private InfoPanel _infoPanel;
    public event Action OnComplete;

    private int   _taskCounter;

    private string _deviceType;

    [Inject]
    private void Construct(InfoPanel infoPanel)
    {
        _infoPanel = infoPanel;
    }

    private void Awake()
    {
        _deviceType =  YandexGame.EnvironmentData.deviceType;
    }

    private void ShowDescription()
    {
        if (_deviceType == ContainerStrings.Desktop || _descriptionPhone == "")
        {
            _infoPanel.ShowMessage(_descriptionDesktop);
        }
        else
        {
            _infoPanel.ShowMessage(_descriptionPhone);
        }
    }

    public void StartTask()
    {
        ShowDescription();
        StartCoroutine(CompletingTasks());

        for (int i = 0; i < _activatingObjects.Length; i++)
        {
            _activatingObjects[i].SetActive(true);
        }
    }

    private IEnumerator CompletingTasks()
    {
        _taskCounter = _activatingObjects.Length;
        _additionalAction?.Invoke();
        yield return new WaitUntil(() => _taskCounter < 1);
        OnComplete?.Invoke();
    }

    public void ClickCounter()
    {
        _taskCounter--;
    }
}
