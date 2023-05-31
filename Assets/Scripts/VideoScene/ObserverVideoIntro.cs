using DG.Tweening;
using UnityEngine;
using Zenject;

public class ObserverVideoIntro : MonoBehaviour
{

    [SerializeField]
    private CarVideo _car;

    [SerializeField]
    private CameraVideoIntro _camera;

    [SerializeField]
    private ZombieVideo _zombie;

    [SerializeField]
    private Transform _fort;

    [SerializeField]
    private UIVideoIntro _uiVideoIntro;

    [SerializeField]
    private float
        _speedCameraBeforeCrush,
        _speedCameraAfterCrush;



    private AudioContainer _audioSource;

    private SceneChanger _sceneChanger;

    [Inject]
    private void Construct(SceneChanger sceneChanger, AudioContainer audio)
    {
        _audioSource = audio;
        _sceneChanger = sceneChanger;
    }

    private void Start()
    {
        _audioSource.PlayMusic(TypeMusic.RailRoads);
        _zombie.OnReachedFirstPoint += OnReachedPointZombie;
        _zombie.OnDead += OnDeadZombie;
        _uiVideoIntro.OnLogoFaded += OnFadedLogo;
    }

    private void OnDisable()
    {
        _zombie.OnReachedFirstPoint -= OnReachedPointZombie;
        _zombie.OnDead -= OnDeadZombie;
        _uiVideoIntro.OnLogoFaded -= OnFadedLogo;
    }


    private void OnReachedPointZombie()
    {
        _car.Ride();
        _camera.Look(_car.transform, _speedCameraBeforeCrush);
    }

    private void OnDeadZombie()
    {
        _audioSource.PlaySound(TypeSound.HitBuilding, 0.5f);
        _camera.Move();
        _camera.Look(_fort, _speedCameraAfterCrush).OnComplete(() =>
        _uiVideoIntro.FadeLogo());
    }

    private void OnFadedLogo()
    {
        _sceneChanger.Change(TypeScene.Fort);
    }
}