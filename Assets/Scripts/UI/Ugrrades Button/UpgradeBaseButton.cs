using UnityEngine;

public class UpgradeBaseButton : UpgradeButton
{
    [SerializeField]
    private TypeUpgradesBuildings _type;

    [SerializeField] 
    private MainHouse _mainHouse;

    protected override void RefreshPanel()
    {
        SaveData
            .BaseUpgrade
            .Upgrade
            .TryGetValue(((int)_type), out bool have);

        if (have)
        {
            LevelUpgrade = 1;
           base.RefreshPanel();
        }
    }

    protected override void OnEnable()
    {
        OnUpgrade +=
            delegate { SaveData.BaseUpgrade.Build(_type); };
        OnUpgrade += 
            delegate { _mainHouse.Upgrade(_type); };

        base.OnEnable();
    }
}