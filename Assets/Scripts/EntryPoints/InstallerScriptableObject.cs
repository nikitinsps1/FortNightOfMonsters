using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "BootstrapScriptableObject", menuName = "Installers/BootstrapScriptableObject")]
public class InstallerScriptableObject : ScriptableObjectInstaller<InstallerScriptableObject>
{
    [SerializeField]
    private LevelContainerSO _levels;

    public override void InstallBindings()
    {
        Container.Bind<LevelContainerSO>().FromInstance(_levels).AsSingle();
    }
}