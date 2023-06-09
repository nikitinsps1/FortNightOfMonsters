using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [Header("Player")]
    [SerializeField] private PlayerHeroLogic _player;
    [SerializeField] private Transform _startPosition;

    [Header("UI")]
    [SerializeField] private WeaponUpgraderContainer _weaponButton;
    [SerializeField] private InteractiveButton _interactiveButton;
    [SerializeField] private BuyMenu _buyMenu;
    [SerializeField] private InfoPanel _infoPanel;
    [SerializeField] private TaskPanel _taskPanel;
    [SerializeField] private DialogMenu _dialogMenu;
    [SerializeField] private HudBars _hudBars;

    [Header("Pools")]
    [SerializeField] private ParticlesContainer _deadParticles;
    [SerializeField] private BulletsContainer _bullets;
    [SerializeField] private EnemiesContainer _enemies;

    [Header("Service")]
    [SerializeField] private LevelProgress _levelProgress;
    [SerializeField] private LevelsContainer _levelsContainer;
    [SerializeField] private LoadSave _loadSave;

    [Header("Other")]
    [SerializeField] private MainHouse _house;

    public override void InstallBindings()
    {
        BindPools();
        BindFort();
        BindPlayer();
        BindService();
        BindUiElements();
    }

    private void BindPools()
    {
        Container.Bind<ParticlesContainer>().FromInstance(_deadParticles).AsSingle();
        Container.Bind<BulletsContainer>().FromInstance(_bullets).AsSingle();
        Container.Bind<EnemiesContainer>().FromInstance(_enemies).AsSingle();
    }

    private void BindService()
    {
        Container.Bind<LevelProgress>().FromInstance(_levelProgress).AsSingle();
        Container.Bind<LevelsContainer>().FromInstance(_levelsContainer).AsSingle();
        Container.Bind<LoadSave>().FromInstance(_loadSave).AsSingle();
    }

    private void BindFort()
    {
        Container.Bind<MainHouse>().FromInstance(_house).AsSingle();
    }

    private void BindPlayer()
    {
        Container.Bind<PlayerHeroLogic>().FromComponentInNewPrefab(_player).AsSingle();
        _player.transform.position = _startPosition.position;
    }

    private void BindUiElements()
    {
        Container.Bind<TaskPanel>().FromInstance(_taskPanel).AsSingle();
        Container.Bind<WeaponUpgraderContainer>().FromInstance(_weaponButton).AsSingle();
        Container.Bind<InteractiveButton>().FromInstance(_interactiveButton).AsSingle();
        Container.Bind<InfoPanel>().FromInstance(_infoPanel).AsSingle();
        Container.Bind<BuyMenu>().FromInstance(_buyMenu).AsSingle();
        Container.Bind<DialogMenu>().FromInstance(_dialogMenu).AsSingle();
        Container.Bind<HudBars>().FromInstance(_hudBars).AsSingle();
    }
}
