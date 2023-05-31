using UnityEngine;
using Zenject;

namespace ZenjectInstaller
{
    public class FortInstaller : MonoInstaller
    {
        [Header("Player")]
        [SerializeField] private Player _player;
        [SerializeField] private Transform _startPosition;
  
        [Header("UI")]
        [SerializeField] private ChangeWeaponPanel _weaponButton;
        [SerializeField] private InteractiveButton _interactiveButton;
        [SerializeField] private BuyMenu _buyMenu;
        [SerializeField] private InfoPanel _infoPanel;

        [Header("Level")]
        [SerializeField] private LevelProgress _levelProgress;

        [Header("Other")]
        [SerializeField] private MainHouse _house;
        [SerializeField] private TaskPanel _taskPanel;


        [SerializeField]
        private DeadParticlesConteiner _deadParticles;

        [SerializeField]
        private BulletsConteiner _bullets;

        [SerializeField]
        private EnemiesPoolsContainer _enemies;




        public override void InstallBindings()
        {
            BindPools();
            BindFort();
            BindPlayer();
            BindLevel();
            BindUiElements();
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






        private void BindPools()
        {
            Container.Bind<DeadParticlesConteiner>()
                .FromInstance(_deadParticles)
                .AsSingle();

            Container.Bind<BulletsConteiner>().
                FromInstance(_bullets)
                .AsSingle();

            Container.Bind<EnemiesPoolsContainer>()
                .FromInstance(_enemies)
                .AsSingle();
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










}