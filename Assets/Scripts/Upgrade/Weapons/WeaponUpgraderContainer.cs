using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgraderContainer : UpgradersContainer
{
    [SerializeField]
    private Upgrader
        _shootGun,
        _riffle,
        _flameThrower;

    private void Awake()
    {
        Upgraders = new Dictionary<int, Upgrader>()
        {
            {(int)TypeWeapons.ShootGun, _shootGun },
            {(int)TypeWeapons.Riffle, _riffle },
            {(int)TypeWeapons.Flamethrower, _flameThrower }
        };
    }
}