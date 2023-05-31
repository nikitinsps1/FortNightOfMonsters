using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    [SerializeField, TextArea(1, 20)]
    private string _descriptionMissions;

    [SerializeField]
    private WaveEnemies[]
        _leftBarrierWaves,
        _rightBarrierWaves;

    [SerializeField]
    private TypeMusic _music;

    [SerializeField]
    private Dialog _dialog;

    [SerializeField]
    private int _reward;

    private WaveEnemies _enemiesOnStartPool;

    public Dictionary<int, WaveEnemies[]> InvasionsSettings
    { get; private set; }

    public Dialog ThisDialog => _dialog;
    public WaveEnemies EnemiesOnStartPool => _enemiesOnStartPool;
    public TypeMusic Music => _music;
    public string DescriptionMissions => _descriptionMissions;
    public int Reward => _reward;

    public void Init()
    {
        WaveEnemies[][] allWaves =
            { _leftBarrierWaves, _rightBarrierWaves };

        for (int i = 0; i < allWaves.Length; i++)
        {
            InitWaves(allWaves[i]);
        }

        InvasionsSettings = new Dictionary<int, WaveEnemies[]>
        {
            {(int)Directions.LeftFlank,  _leftBarrierWaves },
            {(int)Directions.RightFlank, _rightBarrierWaves }
        };

        SumWaves
            (CountMax(ref _leftBarrierWaves), CountMax(ref _rightBarrierWaves));
    }

    private void InitWaves(WaveEnemies[] waves)
    {
        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].Init();
        }
    }

    private void SumWaves(WaveEnemies first,  WaveEnemies second)
    {
        WaveEnemies empty =
            new WaveEnemies();

        _enemiesOnStartPool =
            new WaveEnemies();

        _enemiesOnStartPool
            .ConstructEmptyWave();

        empty.ConstructEmptyWave();

        foreach (var item in _enemiesOnStartPool.Amount)
        {
            int amountEnemy
                = first.Amount[item.Key] + second.Amount[item.Key];

            empty.Amount[item.Key] = amountEnemy;
        }
        _enemiesOnStartPool = empty;
    }

    private WaveEnemies CountMax(ref WaveEnemies[] waves)
    {
        WaveEnemies emptyWave =
            new WaveEnemies();

        emptyWave
            .ConstructEmptyWave();

        for (int i = 0; i < waves.Length; i++)
        {
            foreach (var item in waves[i].Amount)
            {
                if (item.Value > emptyWave.Amount[item.Key])
                {
                    emptyWave.Amount[item.Key] = item.Value;
                }
            }
        }
        return emptyWave;
    }
}