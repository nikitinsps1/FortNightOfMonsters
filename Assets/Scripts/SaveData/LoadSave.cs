public class LoadSave
{
    private SaveData _saveData;

    private UpgradersContainer
        _guards,
        _barricades,
        _characteristics,
        _weapons,
        _fort;

    public LoadSave(
        UpgradersContainer guards, 
        UpgradersContainer barricades,
        UpgradersContainer characteristics,
        UpgradersContainer weapons,
        UpgradersContainer fort,
        SaveData saveData) 
    {
        _guards = guards;
        _barricades = barricades;
        _characteristics = characteristics;
        _weapons = weapons;
        _fort = fort;
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