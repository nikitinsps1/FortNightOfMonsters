using System.Collections.Generic;

public class BaseUpgradeSave
{
    public Dictionary<int, bool> Upgrade;

    public BaseUpgradeSave
        (bool liveHouse = false
        ,bool dynamite = false
        ,bool mainHouseDefense = false)
    {
        Upgrade = new Dictionary<int, bool>
        {
            {((int)TypeUpgradesBuildings.LiveHouse), liveHouse },
            {((int) TypeUpgradesBuildings.Dynamite), dynamite },
            {((int) TypeUpgradesBuildings.MainHouseDefense), mainHouseDefense },
        };
    }

    public void Build(TypeUpgradesBuildings type)
    {
        Upgrade[((int)type)] = true;
    }
}
