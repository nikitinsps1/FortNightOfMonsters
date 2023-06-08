using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [Header("Player")]
    [SerializeField] private Player _player;
    [SerializeField] private Transform _startPosition;

    [Header("UI")]
    [SerializeField] private ChangeWeaponPanel _weaponButton;
    [SerializeField] private InteractiveButton _interactiveButton;
    [SerializeField] private BuyMenu _buyMenu;
    [SerializeField] private InfoPanel _infoPanel;
    [SerializeField] private TaskPanel _taskPanel;

    [Header("Pools")]
    [SerializeField] private ParticlesContainer _deadParticles;
    [SerializeField] private BulletsContainer _bullets;
    [SerializeField] private EnemiesContainer _enemies;

    [Header("Other")]
    [SerializeField] private MainHouse _house;
    [SerializeField] private LevelProgress _levelProgress;

    public override void InstallBindings()
    {
        BindPools();
        BindFort();
        BindPlayer();
        BindLevel();
        BindUiElements();
    }

    private void BindPools()
    {
        Container.Bind<ParticlesContainer>().FromInstance(_deadParticles).AsSingle();
        Container.Bind<BulletsContainer>().FromInstance(_bullets).AsSingle();
        Container.Bind<EnemiesContainer>().FromInstance(_enemies).AsSingle();
    }

    private void BindLevel()
    {
        Container.Bind<LevelProgress>().FromInstance(_levelProgress).AsSingle();
    }

    private void BindFort()
    {
        Container.Bind<MainHouse>().FromInstance(_house).AsSingle();
    }

    private void BindPlayer()
    {
        Container.Bind<Player>().FromComponentInNewPrefab(_player).AsSingle();
        _player.transform.position = _startPosition.position;
    }

    private void BindUiElements()
    {
        Container.Bind<TaskPanel>().FromInstance(_taskPanel).AsSingle();
        Container.Bind<ChangeWeaponPanel>().FromInstance(_weaponButton).AsSingle();
        Container.Bind<InteractiveButton>().FromInstance(_interactiveButton).AsSingle();
        Container.Bind<InfoPanel>().FromInstance(_infoPanel).AsSingle();
        Container.Bind<BuyMenu>().FromInstance(_buyMenu).AsSingle();
    }
}
