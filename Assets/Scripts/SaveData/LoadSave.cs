using UnityEngine;
using Zenject;

public class LoadSave : MonoBehaviour
{
    [SerializeField]
    private UpgradersContainer
        _guards,
        _barricades,
        _characteristics,
        _weapons,
        _fort;

    private SaveData _saveData;

    [Inject]
    private void Construct(SaveData saveData)
    {
        _saveData = saveData;
    }

    private void LoadUpgrades(UpgradersContainer container, SavedObject save)
    {
        foreach (var item in container.Upgraders)
        {
            int level = save.ThisDictionary[item.Key];

            if (level > 0)
            {
                for (int i = 0; i < level; i++)
                {
                    item.Value.Upgrade();
                }
            }
        }
    }

    public void Load()
    {
        LoadUpgrades(_characteristics, _saveData.Characteristics);
        LoadUpgrades(_barricades, _saveData.Barricades);
        LoadUpgrades(_guards, _saveData.Guards);
        LoadUpgrades(_weapons, _saveData.Weapons);
        LoadUpgrades(_fort, _saveData.Fort);
    }
}