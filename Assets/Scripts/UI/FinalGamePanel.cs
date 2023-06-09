using UnityEngine;
using Zenject;

public class FinalGamePanel : MonoBehaviour
{
    [SerializeField]
    private GameObject _panel;

    private SaveData _saveData;
    private int _maxLevel = 12;

    [Inject]
    private void Construct(SaveData saveData)
    {
        _saveData = saveData;

    }

    private void Start()
    {
        if (_saveData.NumberLevel == _maxLevel)
        {
            _panel.SetActive(true);
            _saveData.NewGame();
        }
    }
}
