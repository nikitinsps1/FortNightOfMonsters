using System.Collections.Generic;
using UnityEngine;

/////////Порядок полей изменен ради удобства назначения в инспекторе

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
    private int _reward;

    [SerializeField]
    private Dialog _dialog;

    private WaveEnemies _enemiesStartPool;


    public Dictionary<int, WaveEnemies[]> InvasionsSettings
    { get; private set; }
    public Dialog ThisDialog => _dialog;
    public WaveEnemies EnemiesOnStartPool => _enemiesStartPool;
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
            {(int)Directions.LeftFlank,  _leftBarrierWaves},
            {(int)Directions.RightFlank, _rightBarrierWaves}
        };

        SumWaves(
            CountMax(ref _leftBarrierWaves),
            CountMax(ref _rightBarrierWaves));
    }

    private void InitWaves(WaveEnemies[] waves)
    {
        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].Init();
        }
    }

    private void SumWaves(WaveEnemies first, WaveEnemies second)
    {
        WaveEnemies empty = new WaveEnemies();
        _enemiesStartPool = new WaveEnemies();

        _enemiesStartPool.ConstructEmptyWave();
        empty.ConstructEmptyWave();

        foreach (var item in _enemiesStartPool.Amount)
        {
            int amountEnemy =
                first.Amount[item.Key] + second.Amount[item.Key];

            empty.Amount[item.Key] = amountEnemy;
        }
        _enemiesStartPool = empty;
    }

    private WaveEnemies CountMax(ref WaveEnemies[] waves)
    {
        WaveEnemies amountMaxEnemy = new WaveEnemies();
        amountMaxEnemy.ConstructEmptyWave();

        for (int i = 0; i < waves.Length; i++)
        {
            foreach (var enemyType in waves[i].Amount)
            {
                if (enemyType.Value > amountMaxEnemy.Amount[enemyType.Key])
                {
                    amountMaxEnemy.Amount[enemyType.Key] = enemyType.Value;
                }
            }
        }
        return amountMaxEnemy;
    }
}