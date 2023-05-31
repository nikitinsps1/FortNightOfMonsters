using TMPro;
using UnityEngine;
using Zenject;

public class StartGame : MonoBehaviour
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

    private EnemiesPoolsContainer _enemies;

    private AudioContainer _audio;

    private InfoPanel _info;

    private LevelSettings _levelSettings;
    private LevelProgress _levelProgress;


    private SaveData _saveData;

    [Inject]
    private void Construct(
        InfoPanel infoPanel,
        LevelProgress levelProgress,
        SaveData save, AudioContainer audio, EnemiesPoolsContainer
        enemies)
    {
        _enemies = enemies;
        _info = infoPanel;
        _levelProgress = levelProgress;
        _saveData = save;
        _audio = audio;
    }

    private void Start()
    {
        InitGame();
    }

    public void InitGame()
    {
        Time.timeScale = 1.0f;
        _levelSettings = _levelsContainer.Levels[_saveData.NumberLevel];
        _levelSettings.Init();
        _loadSaveData.Init();
        _levelProgress.Init(_levelSettings, _levelsContainer.Levels.Length);

        _info
            .ShowMessage(_levelSettings.DescriptionMissions);

        int numberDay = _saveData.NumberLevel + 1;
        int amountDays = _levelsContainer.Levels.Length;

        _textStatus.text =
            $"День: {numberDay}/{amountDays}";

        _audio.PlayMusic(_levelSettings.Music);
        FormPools();


        if (_levelSettings.ThisDialog != null)
        {
            InitDialog();
        }
        else
        {
            _levelProgress.ReadyInvasion();
        }

        _saveData.SendServer();
    }

    private void InitDialog()
    {
        _dialogMenu.Init(_levelSettings.ThisDialog);

        _dialogsPlace.gameObject.SetActive(true);

        Instantiate(
            _levelSettings.ThisDialog.Character,
            _dialogsPlace);

        _levelProgress.SetNewTask("У входа в лагерь вас ждет гость");
    }

    private void FormPools()
    {
        foreach (var item in _levelSettings.EnemiesOnStartPool.Amount)
        {
            _enemies.Pools[item.Key].FormPool(item.Value);
        }
    }
}