using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [Header("Player")]
    [SerializeField] private PlayerHeroLogic _player;
    [SerializeField] private Transform _startPosition;

    [Header("UI")]
    [SerializeField] private InteractiveButton _interactiveButton;
    [SerializeField] private BuyMenu _buyMenu;
    [SerializeField] private InfoPanel _infoPanel;
    [SerializeField] private TaskPanel _taskPanel;
    [SerializeField] private DialogMenu _dialogMenu;
    [SerializeField] private HudBars _hudBars;

    [Header("Upgraders")]
    [SerializeField] private WeaponUpgraderContainer _weaponButton;
    [SerializeField] private CharacteristicUpgraderContainer _characteristicsUpgraders;
    [SerializeField] private FortUpgraderContainer _fortUpgraders;

    [Header("Pools")]
    [SerializeField] private ParticlesContainer _deadParticles;
    [SerializeField] private BulletsContainer _bullets;
    [SerializeField] private EnemiesContainer _enemies;

    [Header("Service")]
    [SerializeField] private LevelProgress _levelProgress;

    [Header("Other")]
    [SerializeField] private MainHouse _house;

    public override void InstallBindings()
    {
        BindUpgraders();
        BindPools();
        BindFort();
        BindPlayer();
        BindService();
        BindUiElements();
    }

    private void BindUpgraders()
    {
        Container.Bind<WeaponUpgraderContainer>().FromInstance(_weaponButton).AsSingle();
        Container.Bind<CharacteristicUpgraderContainer>().FromInstance(_characteristicsUpgraders).AsSingle();
        Container.Bind<FortUpgraderContainer>().FromInstance(_fortUpgraders).AsSingle();
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
        Container.Bind<InteractiveButton>().FromInstance(_interactiveButton).AsSingle();
        Container.Bind<InfoPanel>().FromInstance(_infoPanel).AsSingle();
        Container.Bind<BuyMenu>().FromInstance(_buyMenu).AsSingle();
        Container.Bind<DialogMenu>().FromInstance(_dialogMenu).AsSingle();
        Container.Bind<HudBars>().FromInstance(_hudBars).AsSingle();
    }
}
