using System.Collections.Generic;

public class BaseUpgradeSave
{
    public Dictionary<int, bool> Upgrades;

    public BaseUpgradeSave
        (bool liveHouse = false
        ,bool dynamite = false
        ,bool mainHouseDefense = false)
    {
        Upgrades = new Dictionary<int, bool>
        {
            {(int)TypeUpgradesBuildings.LiveHouse, liveHouse},
            {(int) TypeUpgradesBuildings.Dynamite, dynamite},
            {(int) TypeUpgradesBuildings.MainHouseDefense, mainHouseDefense},
        };
    }

    public void Build(TypeUpgradesBuildings type)
    {
        Upgrades[(int)type] = true;
    }
}
