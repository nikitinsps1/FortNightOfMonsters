using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    private EnemiesContainer _pool;

    private Transform _transform;
    private List<EnemyLogic> _spawnedEnemies;

    private float _radiusSpawn = 10.5f;

    [Inject]
    private void Construct(EnemiesContainer enemies)
    {
        _pool = enemies;
    }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _spawnedEnemies = new List<EnemyLogic>();
        _transform = transform;
    }

    public List<EnemyLogic> Spawn(ref WaveEnemies sizeWave)
    {
        _spawnedEnemies.Clear();

        EnemyLogic enemy;
        Vector3 spawnPosition;

        foreach (var item in sizeWave.Amount)
        {
            for (int i = 0; i < item.Value; i++)
            {
                spawnPosition =
                    _transform.position + Random.insideUnitSphere * _radiusSpawn;

                enemy = _pool.GetObject(
                    item.Key, spawnPosition, _transform.rotation)
                    .GetComponent<EnemyLogic>();

                _spawnedEnemies.Add(enemy);
            }
        }
        return _spawnedEnemies;
    }
}