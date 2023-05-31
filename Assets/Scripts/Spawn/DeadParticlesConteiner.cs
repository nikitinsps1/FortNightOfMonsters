using System.Collections.Generic;
using UnityEngine;

public enum TypeDeadPartiecles
{
    Boom = 0,
    DeadZombie  = 1,
    DeadPeople = 2,
    DeadSpider = 3,
    BuildingBoom = 4
}

public class DeadParticlesConteiner : PoolContainer<TypeDeadPartiecles>
{
    [SerializeField]
    private Pool
        _boom,
        _deadHuman,
        _deadZombie,
        _deadSpider,
        _buildingBoom;

    protected override void InitDictionary()
    {

        Pools = new Dictionary<int, Pool>()
        {
            {((int)TypeDeadPartiecles.Boom), _boom},
            {((int)TypeDeadPartiecles.DeadZombie), _deadZombie},
            {((int)TypeDeadPartiecles.DeadPeople), _deadHuman},
            {((int)TypeDeadPartiecles.DeadSpider), _deadSpider},
            {((int)TypeDeadPartiecles.BuildingBoom), _buildingBoom}
        };
        base.InitDictionary();
    }
}