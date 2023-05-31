﻿using System;
using UnityEngine;
using Zenject;

public class DialogAssault : DialogAction
{
    [SerializeField]
    private Directions _direction;

    [SerializeField] 
    private WaveEnemies[] _waveEnemies;

    [SerializeField] 
    private string _textTask;

    private LevelProgress _progress;
    private EnemiesPoolsContainer _enemies;

    [Inject]
    private void Construct(LevelProgress progress , EnemiesPoolsContainer enemies)
    {
        _progress = progress;
        _enemies = enemies;
    }


    public override Action GetEvent()
    {
        for (int i = 0; i < _waveEnemies.Length; i++)
        {
            _waveEnemies[i].Init();

            _enemies.Pools[(int)TypeEnemy.BanditGun].FormPool(5);
            _enemies.Pools[(int)TypeEnemy.BanditMele].FormPool(10);
        }

        HaveNewTask = true;

        Action action = 
            delegate 
            {
                _progress
                .SetNewTask(_textTask, _direction, _waveEnemies); 
            };

        return action;
    }
}