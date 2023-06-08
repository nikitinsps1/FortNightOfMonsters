using Zenject;
using UnityEngine;

public class TutorialInstaller : MonoInstaller
{
    [Header("Player")]
    [SerializeField] private Player _player;
    [SerializeField] private Transform _startPosition;

    [Header("UI")]
    [SerializeField] private InfoPanel _infoPanel;
    [SerializeField] private InteractiveButton _interactiveButton;
    [SerializeField] private ChangeWeaponPanel _changeWeapon;

    [Header("Other")]
    [SerializeField] private TutorialObserver _tutorial;
    [SerializeField] private ParticlesContainer _deadParticles;


    public override void InstallBindings()
    {
        Container.Bind<ParticlesContainer>().FromInstance(_deadParticles).AsSingle();
        BindUI();
        BindPlayer();
        Container.Bind<TutorialObserver>().FromInstance(_tutorial).AsSingle();
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

