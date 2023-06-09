using System.Collections.Generic;

public class FortUpgradeSave: SavedObject
{
    public FortUpgradeSave(
        int liveHouse = 0,
        int dynamite = 0,
        int mainHouseDefense = 0)
    {
        ThisDictionary = new Dictionary<int, int>
        {
            {(int)TypeFortUpgrade.LiveHouse, liveHouse},
            {(int) TypeFortUpgrade.Dynamite, dynamite},
            {(int) TypeFortUpgrade.DefenseBag, mainHouseDefense},
        };
    }

}
