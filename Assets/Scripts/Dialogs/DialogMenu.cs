using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(DialogActionMediator))]
public class DialogMenu : MonoBehaviour
{
    [SerializeField]
    private Button
        _confirm,
        _cancel,
        _charisma;

    [SerializeField]
    private TextMeshProUGUI _tmpDialog;

    private DialogActionMediator _mediator;

    private DialogSO _dialog;
    private SaveData _data;
    private LevelProgress _levelProgress;

    private Dictionary<int, Button> _buttonsDialog;

    [Inject]
    private void Construct(SaveData data, LevelProgress levelProgress)
    {
        _data = data;
        _levelProgress = levelProgress;
    }

    private void OnEnable()
    {
        CheckCharisma();
    }

    private void CheckCharisma()
    {
        int charismaValue = (_data
            .Characteristics
            .ThisDictionary
            [(int)TypeCharacteristics.Charisma]);

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

        button.onClick.AddListener(() => ResultDialog(setting));
    }

    private void ChangeText(string newText)
    {
        _tmpDialog.text = newText;
    }

    public void ResultDialog(DialogButtonSetting setting)
    {
        ChangeText(setting.TextAfter);

        if (setting.DialogAction != null)
            setting.DialogAction.GetAction(_mediator).Invoke();

        if (setting.HaveNewTask == false)
            _levelProgress.ReadyInvasion();
    }

    public void Init(DialogSO dialog)
    {
        _mediator = GetComponent<DialogActionMediator>();
        _dialog = dialog;
        _dialog.Init();

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