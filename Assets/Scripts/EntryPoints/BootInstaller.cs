using UnityEngine;
using Zenject;

public class BootInstaller : MonoInstaller
{
    [SerializeField]
    private SaveData _saveData;

    [SerializeField]
    private SceneChanger _sceneChanger;

    [SerializeField]
    private AudioContainer _audioContainer;

    public override void InstallBindings()
    {
        Container.Bind<SaveData>()
            .FromComponentInNewPrefab(_saveData)
            .AsSingle()
            .NonLazy();

        Container.Bind<SceneChanger>()
          .FromComponentInNewPrefab(_sceneChanger)
          .AsSingle();

        Container.Bind<AudioContainer>()
            .FromComponentInNewPrefab(_audioContainer)
            .AsSingle();
    }
}