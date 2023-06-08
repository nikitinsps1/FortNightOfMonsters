using System.Collections.Generic;
using UnityEngine;

public enum TypeBullets
{
    Spite = 0,
    BulletGun = 1,
    Explosion = 2
}

public class BulletsContainer : PoolContainer<TypeBullets>
{
    [SerializeField]
    private Pool
    _spite,
    _bulletGun,
    _explosion;

    protected override void InitDictionary()
    { 
        Pools = new Dictionary<int, Pool>()
        {
            {(int)TypeBullets.Spite, _spite},
            {(int)TypeBullets.BulletGun, _bulletGun},
            {(int)TypeBullets.Explosion, _explosion}
        };
        base.InitDictionary();
    }
}
