using UnityEngine;
using Zenject;

public class UpgradeBase : Upgrade
{
    [SerializeField]
    private TypeUpgradesBuildings _type;

    private MainHouse _mainHouse;

    [Inject]
    private void Construct(MainHouse house)
    {
        _mainHouse = house;
    }

    protected override void RefreshPanel()
    {
        SaveData.BaseUpgrade.Upgrades
            .TryGetValue((int)_type, out bool isHaveUpgrade);

        if (isHaveUpgrade)
        {
            LevelUpgrade = 1;
           base.RefreshPanel();
        }
    }

    protected override void OnEnable()
    {
        OnUpgrade += delegate 
        {SaveData.BaseUpgrade.Build(_type);};

        OnUpgrade += delegate 
        { _mainHouse.Upgrade(_type); };

        base.OnEnable();
    }
}