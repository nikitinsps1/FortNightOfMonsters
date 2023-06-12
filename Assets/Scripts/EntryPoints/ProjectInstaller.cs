using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField]
    private SceneLoader _sceneChanger;

    [SerializeField]
    private AudioContainer _audioContainer;

    private SaveData _saveData;

    public override void InstallBindings()
    {
        _saveData = new SaveData();
        Container.Bind<SaveData>().FromInstance(_saveData).AsSingle();

        Container.Bind<SceneLoader>()
          .FromComponentInNewPrefab(_sceneChanger)
          .AsSingle();

        Container.Bind<AudioContainer>()
            .FromComponentInNewPrefab(_audioContainer)
            .AsSingle();
    }
}