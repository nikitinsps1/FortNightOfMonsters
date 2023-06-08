using TMPro;
using UnityEngine;
using Zenject;

public class InitLevel : MonoBehaviour
{
    [SerializeField]
    private Transform
        _dialogsPlace;

    [SerializeField]
    private TextMeshProUGUI
    _textStatus;

    [SerializeField]
    private LevelsContainer _levelsContainer;

    [SerializeField]
    private LoadSaveData _loadSaveData;

    [SerializeField]
    private DialogMenu _dialogMenu;

    private EnemiesContainer _enemies;
    private AudioContainer _audio;
    private InfoPanel _info;

    private LevelSettings _levelSettings;
    private LevelProgress _levelProgress;

    private SaveData _saveData;

    [Inject]
    private void Construct(
        InfoPanel infoPanel,
        LevelProgress levelProgress,
        SaveData save,
        AudioContainer audio,
        EnemiesContainer enemies)
    {
        _enemies = enemies;
        _info = infoPanel;
        _levelProgress = levelProgress;
        _saveData = save;
        _audio = audio;
    }

    private void Start()
    {
        _levelSettings =
            _levelsContainer.Levels[_saveData.NumberLevel];

        _levelSettings.Init();

        _levelProgress.Init
            (_levelSettings, _levelsContainer.Levels.Length);

        _loadSaveData.Load();
        _audio.PlayMusic(_levelSettings.Music);

        InitDialog();
        SetNumberDay();
        FormPools();

        _info.ShowMessage
            (_levelSettings.DescriptionMissions);

        _saveData.SendServer();
    }

    private void SetNumberDay()
    {
        int numberDay = _saveData.NumberLevel + 1;
        int amountDays = _levelsContainer.Levels.Length;

        _textStatus.text =
            $"День: {numberDay}/{amountDays}";
    }

    private void InitDialog()
    {
        if (_levelSettings.ThisDialog != null)
        {
            _dialogMenu.Init(_levelSettings.ThisDialog);

            _dialogsPlace.gameObject.SetActive(true);

            Instantiate(
                _levelSettings.ThisDialog.Character,
                _dialogsPlace);

            _levelProgress.SetNewTask("У входа в лагерь вас ждет гость");
        }
        else
        {
            _levelProgress.ReadyInvasion();
        }
    }

    private void FormPools()
    {
        foreach (var item in _levelSettings.EnemiesOnStartPool.Amount)
        {
            _enemies.Pools[item.Key].FormPool(item.Value);
        }
    }
}