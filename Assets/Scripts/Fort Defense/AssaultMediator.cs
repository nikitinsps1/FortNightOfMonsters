using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(EnemySpawner))]
public class AssaultMediator : MonoBehaviour
{
    [SerializeField]
    private Directions _direction;

    [SerializeField]
    private Barrier _barrier;
    [SerializeField]
    private AudioContainer _audio;

    private List<Damageable>
        _enemies,
        _frontiers;

    private EnemySpawner _enemySpawner;
    private LevelProgress _levelProgress;

    private WaveEnemies[] _waves;

    private int _wavesCounter;
    private bool _barrierIsBroken;

    public event Action OnBrokenBarrier;

    [Inject]
    private void Construct(LevelProgress progress)
    {
        _levelProgress = progress;
    }

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        _enemies = new List<Damageable>();
        _enemySpawner = GetComponent<EnemySpawner>();
    }

    private void InitTeams()
    {
        _frontiers = _barrier.FormFrontiers();

        List<Enemy> enemies
            = _enemySpawner.Spawn(ref _waves[_wavesCounter]);

        for (int i = 0; i < enemies.Count; i++)
        {
            _enemies.Add(enemies[i].ThisDamageable);


            enemies[i]
                .StartAssault(this, _barrierIsBroken);
        }
    }

    private List<Damageable> ChooseTeam(TypeRealations typeRelations)
    {
        if (typeRelations == TypeRealations.Enemy)
        {
           return _enemies;
        }
        else
        {
            return _frontiers;
        }
    }

    private void CheckEndWave()
    {
        if (_enemies.Count == 0)
        {
            if (_waves.Length - 1 > _wavesCounter)
            {
                _wavesCounter++;
                InitTeams();
                _barrier.Alarm();
            }
            else
            {
                _levelProgress.InvasionComplete();
            }
        }
    }

    public void StartInvasion( WaveEnemies[] waves)
    {
        if (waves.Length == 0)
        {
            _levelProgress.InvasionComplete();
        }
        else
        {
            _waves = waves;
            InitTeams();
            _barrier.Alarm();
        }
    }

    public Damageable GetTarget(TypeRealations relationTarget)
    {
        List<Damageable> currentTeam =
            ChooseTeam(relationTarget); 

        if (currentTeam.Count>0)
        {
            return currentTeam[0];
        }
        else
        {
            return null;
        }
    }

    public void RemoveTarget(Damageable dying)
    {
        List<Damageable> currentTeam = ChooseTeam(dying.Relation);

        currentTeam.Remove(dying);

        if (currentTeam == _enemies)
        {
            CheckEndWave();
        }
    }

    public void BrokeBarrier() 
    {
        OnBrokenBarrier?.Invoke();
        _barrierIsBroken = true;
    }
}