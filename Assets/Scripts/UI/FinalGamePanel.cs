using UnityEngine;
using Zenject;

public class FinalGamePanel : MonoBehaviour
{
    [SerializeField]
    private GameObject _panel;

    private SaveData _saveData;

    [Inject]
    private void Construct(SaveData saveData )
    {
        _saveData = saveData;

    }

    private void Start()
    {
        if (_saveData.NumberLevel == 12)
        {
            _panel.SetActive(true);
            _saveData.NewGame();
        }
    }
}
