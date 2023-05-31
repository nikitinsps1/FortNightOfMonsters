using UnityEngine;
using Zenject;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _endMenu;

    private SaveData _saveData;


    [Inject]
    private void Construct(SaveData saveData )
    {
        _saveData = saveData;

    }



    private void Start()
    {
        Time.timeScale = 1.0f;

        if (_saveData.NumberLevel == 12)
        {
            _endMenu.SetActive(true);
            _saveData.NewGame();
        }
    }
}
