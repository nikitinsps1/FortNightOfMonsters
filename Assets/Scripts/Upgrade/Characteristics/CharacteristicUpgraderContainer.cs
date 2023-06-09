using System.Collections.Generic;
using UnityEngine;

public class CharacteristicUpgraderContainer : UpgradersContainer
{
    [SerializeField]
    private Upgrader
        _health,
        _charisma;

    private void Awake()
    {
        Upgraders = new Dictionary<int, Upgrader>()
        {
            {(int)TypeCharacteristicks.Health, _health },
            {(int)TypeCharacteristicks.Charisma, _charisma },
        };
    }
}
