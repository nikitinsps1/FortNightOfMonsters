using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DialogMenu : MonoBehaviour
{
    [SerializeField]
    private Button
        _confirm,
        _cancel,
        _charisma;

    [SerializeField]
    private TextMeshProUGUI _tmpDialog;

    [SerializeField]
    private Button _endDialogButton;

    [SerializeField]
    private GameObject _characterPlace;

    private Dialog _dialog;
    private SaveData _data;
    private TaskPanel _taskPanel;

    private Dictionary
        <int, Button>
        _buttonsDialog;

    [Inject]
    private void Construct(SaveData data, TaskPanel taskPanel)
    {
        _data = data;
        _taskPanel = taskPanel;
    }

    private void OnEnable()
    {
        CheckCharisma();
    }

    private void CheckCharisma()
    {
        int charismaValue = 
            ((int)_data.Characteristics.Levels
            [(int)TypeCharacteristicks.Charisma]);

        if (charismaValue < _dialog.RequiredCharisma)
        {
            _charisma.interactable = false;
        }
        else
        {
            _charisma.interactable = true;
        }
    }

    private void InitializeButton(int indexButton, Button button)
    {
        DialogButtonSetting setting = _dialog.Settings[indexButton];

        Action resultDialog = delegate
        { ChangeText(setting.TextAfter); };

        if (setting.DialogAction != null)
        {
            resultDialog += setting.DialogAction.GetEvent();
        }

        resultDialog += delegate
        { EndDialog(setting.HaveNewTask);};

        button.onClick.AddListener(resultDialog.Invoke);
    }

    private void EndDialog(bool haveNewTask)
    {
        foreach (var item in _buttonsDialog)
        {
            item.Value.gameObject.SetActive(false);
        }

        _endDialogButton.gameObject.SetActive(true);

        _characterPlace.SetActive(false);

        if (haveNewTask == false)
        {
            _taskPanel.OnReadyForInvasions();
        }
    }

    private void ChangeText(string newText)
    {
        _tmpDialog.text = newText;

    }

    public void Init(Dialog dialog)
    {
        _dialog = dialog;
        dialog.Init();

        _buttonsDialog = new Dictionary<int, Button>
        {
          {(int)TypeAnswersDialog.Confirm, _confirm},
          {(int) TypeAnswersDialog.Cancel, _cancel },
          {(int) TypeAnswersDialog.Charisma, _charisma }
        };

        foreach (var item in _buttonsDialog)
        {
            InitializeButton(item.Key, item.Value);
        }

        ChangeText(_dialog.StartSpeech);
    }
}