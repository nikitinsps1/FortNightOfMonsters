using UnityEngine;
using Zenject;

public class DeleteSave : MonoBehaviour
{
    private SaveData _saveData;

    [Inject]
    private void Construct(SaveData saveData)
    {
        _saveData = saveData;
    }

    public void Delete()
    {
        _saveData.NewGame();
        _saveData.SendServer();
    }
}
