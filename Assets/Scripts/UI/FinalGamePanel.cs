using UnityEngine;
using Zenject;

public class FinalGamePanel : MonoBehaviour
{
    [SerializeField]
    private GameObject _panel;

    private SaveData _saveData;
    private LevelContainerSO _levelContainer;

    [Inject]
    private void Construct(SaveData saveData, LevelContainerSO levelContainerSO)
    {
        _saveData = saveData;
        _levelContainer = levelContainerSO;
    }

    private void Start()
    {
        if (_saveData.NumberLevel == _levelContainer.Levels.Length - 1)
        {
            _panel.SetActive(true);
            _saveData.NewGame();
        }
    }
}
