using System.Collections.Generic;
using UnityEngine;

public enum TypeDeadParticles
{
    Boom = 0,
    DeadZombie  = 1,
    DeadPeople = 2,
    DeadSpider = 3,
    BuildingBoom = 4,
    Hit = 5
}

public class ParticlesContainer : PoolContainer<TypeDeadParticles>
{
    [SerializeField]
    private Pool
        _boom,
        _deadHuman,
        _deadZombie,
        _deadSpider,
        _buildingBoom,
        _hit;

    protected override void InitDictionary()
    {
        Pools = new Dictionary<int, Pool>()
        {
            {(int)TypeDeadParticles.Boom, _boom},
            {(int)TypeDeadParticles.DeadZombie, _deadZombie},
            {(int)TypeDeadParticles.DeadPeople, _deadHuman},
            {(int)TypeDeadParticles.DeadSpider, _deadSpider},
            {(int)TypeDeadParticles.BuildingBoom, _buildingBoom},
            {(int)TypeDeadParticles.Hit, _hit}
        };
        base.InitDictionary();
    }
}