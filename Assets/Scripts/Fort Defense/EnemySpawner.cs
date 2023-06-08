using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    private EnemiesContainer _pool;

    private Transform _transform;
    private List<Enemy> _spawnedEnemies;

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
        _spawnedEnemies = new List<Enemy>();
        _transform = transform;
    }

    public List<Enemy> Spawn(ref WaveEnemies sizeWave)
    {
        _spawnedEnemies.Clear();

        Enemy enemy;
        Vector3 spawnPosition;

        foreach (var item in sizeWave.Amount)
        {
            for (int i = 0; i < item.Value; i++)
            {
                spawnPosition 
                    = _transform.position + Random.insideUnitSphere * _radiusSpawn;

                enemy = _pool.GetObject
                    (item.Key, spawnPosition, _transform.rotation)
                    .GetComponent<Enemy>();
                  
                _spawnedEnemies.Add(enemy);
            }
        }
        return _spawnedEnemies;
    }
}