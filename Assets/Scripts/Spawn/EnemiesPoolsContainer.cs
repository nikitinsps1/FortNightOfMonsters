using System.Collections.Generic;
using UnityEngine;

public enum TypeEnemy
{
    Zombie = 0,
    Kamikaze = 1,
    Cyborg = 2,
    Spider = 3,
    BanditGun = 4,
    BanditMele = 5
}

public class EnemiesPoolsContainer : PoolContainer<TypeEnemy>
{
    [SerializeField]
    private Pool
        _zombies,
        _kamikaze,
        _cyborgs,
        _spiders,
        _gunBandits,
        _meleBandits;

    protected override void InitDictionary()
    {
       
        Pools = new Dictionary<int, Pool>()
        {
            {((int)TypeEnemy.Zombie), _zombies },
            {((int)TypeEnemy.Kamikaze), _kamikaze },
            {((int)TypeEnemy.Cyborg), _cyborgs},
            {((int)TypeEnemy.Spider), _spiders},
            {((int)TypeEnemy.BanditGun), _gunBandits},
            {((int)TypeEnemy.BanditMele), _meleBandits},
        };
        base.InitDictionary();

    }
}