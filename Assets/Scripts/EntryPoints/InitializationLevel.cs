using TMPro;
using UnityEngine;
using Zenject;

public class InitializationLevel : MonoBehaviour
{
    [SerializeField]
    private Transform _dialogsPlace;

    [SerializeField]
    private TextMeshProUGUI _textStatus;

    [SerializeField]
    private UpgradersContainer
        _guards,
        _barricades;

    private UpgradersContainer
        _characteristics,
        _weapons,
        _fort;

    private DialogMenu _dialogMenu;

    private EnemiesContainer _enemies;
    private AudioContainer _audio;
    private InfoPanel _info;

    private LevelSettingSO level;
    private LevelProgress _levelProgress;

    private SaveData _saveData;
    private LoadSave _loadSave;
    private LevelContainerSO _levelContainerSO;

    [Inject]
    private void Construct(
        InfoPanel infoPanel,
        LevelProgress levelProgress,
        SaveData save,
        AudioContainer audio,
        EnemiesContainer enemies,
        DialogMenu dialog,
        LevelContainerSO levelContainer,
        CharacteristicUpgraderContainer characteristicUpgraders,
        WeaponUpgraderContainer weaponUpgraders,
        FortUpgraderContainer fortUpgraderContainer)
    {
        _enemies = enemies;
        _info = infoPanel;
        _levelProgress = levelProgress;
        _saveData = save;
        _audio = audio;
        _dialogMenu = dialog;
        _levelContainerSO = levelContainer;
        _characteristics = characteristicUpgraders;
        _weapons = weaponUpgraders;
        _fort = fortUpgraderContainer;
    }

    private void Start()
    {
        level = _levelContainerSO.Levels[_saveData.NumberLevel];
        level.Init();
        _levelProgress
            .Init(level, _levelContainerSO.Levels.Length);

        LoadSave();

        InitDialog();
        SetNumberDay();
        FormPools();

        _info.ShowMessage(level.Description);

        _saveData.SendServer();
        _audio.PlayMusic(level.Music);
    }

    private void LoadSave()
    {
        _loadSave = new LoadSave(
            _guards,
            _barricades,
            _characteristics,
            _weapons,
            _fort,
            _saveData);

        _loadSave.Load();
    }

    private void SetNumberDay()
    {
        int numberDay = _saveData.NumberLevel + 1;
        int amountDays = _levelContainerSO.Levels.Length;

        _textStatus.text =
            $"День: {numberDay}/{amountDays}";
    }

    private void InitDialog()
    {
        if (level.ThisDialog != null)
        {
            _dialogMenu.Init(level.ThisDialog);
            _dialogsPlace.gameObject.SetActive(true);

            Instantiate(
                level.ThisDialog.Character,
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
        foreach (var item in level.EnemiesStartPool.Amount)
        {
            _enemies
                .Pools[item.Key]
                .FormPool(item.Value);
        }
    }
}