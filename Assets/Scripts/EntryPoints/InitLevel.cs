using TMPro;
using UnityEngine;
using Zenject;

public class InitLevel : MonoBehaviour
{
    [SerializeField]
    private Transform _dialogsPlace;

    [SerializeField]
    private TextMeshProUGUI _textStatus;

    private DialogMenu _dialogMenu;

    private EnemiesContainer _enemies;
    private AudioContainer _audio;
    private InfoPanel _info;

    private LevelsContainer _levelsContainer;
    private LevelSettings _levelSettings;
    private LevelProgress _levelProgress;

    private SaveData _saveData;
    private LoadSave _loadSave;


    [Inject]
    private void Construct(
        InfoPanel infoPanel,
        LevelProgress levelProgress,
        SaveData save,
        AudioContainer audio,
        EnemiesContainer enemies,
        LevelsContainer levelsContainer,
        LoadSave loadSave,
        DialogMenu dialog)
    {
        _enemies = enemies;
        _info = infoPanel;
        _levelProgress = levelProgress;
        _saveData = save;
        _audio = audio;
        _levelsContainer = levelsContainer;
        _loadSave = loadSave;
        _dialogMenu = dialog;
    }

    private void Start()
    {
        _levelSettings =
            _levelsContainer.Levels[_saveData.NumberLevel];

        _levelSettings.Init();

        _levelProgress
            .Init(_levelSettings, _levelsContainer.Levels.Length);

        _loadSave.Load();

        InitDialog();
        SetNumberDay();
        FormPools();

        _info
            .ShowMessage(_levelSettings.DescriptionMissions);

        _saveData.SendServer();
        _audio.PlayMusic(_levelSettings.Music);
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
            _enemies
                .Pools[item.Key]
                .FormPool(item.Value);
        }
    }
}