using Zenject;
using UnityEngine;

namespace ZenjectInstaller
{
    public class TutorialInstaller : MonoInstaller
    {
        [SerializeField] private Player _player;
        [SerializeField] private Transform _startPosition;


        [SerializeField] private Tutorial _tutorial;

        [SerializeField] private InfoPanel _infoPanel;
        [SerializeField] private InteractiveButton _interactiveButton;
        [SerializeField] private ChangeWeaponPanel _changeWeapon;


        [SerializeField]
        private DeadParticlesConteiner _deadParticles;
        public override void InstallBindings()
        {
            Container.Bind<DeadParticlesConteiner>()
                .FromInstance(_deadParticles)
                .AsSingle();

            BindUI();
            Container.Bind<Tutorial>().FromInstance(_tutorial).AsSingle();

            BindPlayer();
        }

        private void BindUI()
        {
            Container.Bind<InfoPanel>().FromInstance(_infoPanel).AsSingle();
            Container.Bind<ChangeWeaponPanel>().FromInstance(_changeWeapon).AsSingle();
            Container.Bind<InteractiveButton>().FromInstance(_interactiveButton).AsSingle();
        }

        private void BindPlayer()
        {
            Container.Bind<Player>().FromComponentInNewPrefab(_player).AsSingle();
            _player.transform.position = _startPosition.position;
        }
    }
}
