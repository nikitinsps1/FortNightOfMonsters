using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog", menuName = "Level/Create new Dialog")]
public class DialogSO : ScriptableObject
{
    [SerializeField, TextArea(1, 20)]
    private string _startSpeech;

    [SerializeField]
    private GameObject _character;

    [SerializeField]
    private int _requiredCharisma;

    public GameObject Character => _character;
    public string StartSpeech => _startSpeech;
    public int RequiredCharisma => _requiredCharisma;

    [SerializeField]
    private DialogButtonSetting
        _confirm,
        _cancel,
        _charisma;

    public Dictionary<int, DialogButtonSetting> Settings
    { get; private set; }

    public void Init()
    {
        Settings = new Dictionary<int, DialogButtonSetting>
        {
            {(int) TypeAnswersDialog.Confirm, _confirm},
            {(int) TypeAnswersDialog.Cancel, _cancel},
            {(int) TypeAnswersDialog.Charisma, _charisma}
        };
    }
}
