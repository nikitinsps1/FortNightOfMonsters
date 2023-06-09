using System;
using UnityEngine;
using Zenject;

public class DialogBanditAssault : DialogAction
{
    [SerializeField]
    private Directions _direction;

    [SerializeField]
    private WaveEnemies[] _waveEnemies;

    [SerializeField]
    private string _textTask;

    private LevelProgress _progress;
    private EnemiesContainer _enemies;

    [Inject]
    private void Construct(LevelProgress progress, EnemiesContainer enemies)
    {
        _progress = progress;
        _enemies = enemies;
    }

    public override Action GetAction()
    {
        IsHaveNewTask = true;

        _enemies.Pools[(int)TypeEnemy.BanditGun].FormPool(5);
        _enemies.Pools[(int)TypeEnemy.BanditMele].FormPool(10);

        for (int i = 0; i < _waveEnemies.Length; i++)
        {
            _waveEnemies[i].Init();
        }

        return delegate
        {
            _progress.SetNewTask(_textTask, _direction, _waveEnemies);
        };
    }
}