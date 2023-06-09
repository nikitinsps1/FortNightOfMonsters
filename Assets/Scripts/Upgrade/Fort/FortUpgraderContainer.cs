using System.Collections.Generic;
using UnityEngine;

public class FortUpgraderContainer : UpgradersContainer
{
    [SerializeField]
    private Upgrader
     _dynamite,
     _liveHouse,
     _defenseBags;

    private void Awake()
    {
        Upgraders = new Dictionary<int, Upgrader>()
        {
            {(int)TypeFortUpgrade.Dynamite, _dynamite },
            {(int)TypeFortUpgrade.LiveHouse, _liveHouse },
            {(int)TypeFortUpgrade.DefenseBag, _defenseBags }
        };
    }
}
