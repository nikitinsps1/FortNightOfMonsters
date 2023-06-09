using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct WaveEnemies
{
    [SerializeField]
    private int
        _zombies,
        _kamikazes,
        _spiders,
        _cyborgs,
        _banditGun,
        _banditMelee;

    public Dictionary<int, int> Amount
    { get; private set; }

    public void Init()
    {
        Amount = new Dictionary<int, int>()
            {
                {(int)TypeEnemy.Zombie, _zombies },
                {(int)TypeEnemy.Kamikaze,_kamikazes },
                {(int)TypeEnemy.Spider, _spiders },
                {(int)TypeEnemy.Cyborg, _cyborgs },
                {(int)TypeEnemy.BanditGun, _banditGun },
                {(int)TypeEnemy.BanditMele, _banditMelee }
            };
    }

    public void ConstructEmptyWave()
    {
        Amount = new Dictionary<int, int>()
            {
                {(int)TypeEnemy.Zombie, 0 },
                {(int)TypeEnemy.Kamikaze,0 },
                {(int)TypeEnemy.Spider, 0 },
                {(int)TypeEnemy.Cyborg, 0 },
                {(int)TypeEnemy.BanditGun, 0 },
                {(int)TypeEnemy.BanditMele, 0 }
            };
    }
}