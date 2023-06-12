using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Level/Create new Level")]
public class LevelSettingSO : ScriptableObject
{
    [SerializeField, TextArea(1, 20)]
    private string _description;

    [SerializeField]
    private WaveEnemies[]
        _leftBarrierWaves,
        _rightBarrierWaves;

    [SerializeField]
    private TypeMusic _music;

    [SerializeField]
    private int _reward;

    [SerializeField]
    private DialogSO _dialog;

    private WaveEnemies _enemiesStartPool;

    public Dictionary<int, WaveEnemies[]> InvasionsSettings
    { get; private set; }

    public DialogSO ThisDialog => _dialog;
    public WaveEnemies EnemiesStartPool => _enemiesStartPool;
    public TypeMusic Music => _music;
    public string Description => _description;
    public int Reward => _reward;

    private void InitWaves(WaveEnemies[] waves)
    {
        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].Init();
        }
    }

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

        WaveOperations waveOperations = new WaveOperations();

        _enemiesStartPool =
            waveOperations.SumWaves(
            waveOperations.CountMax(ref _leftBarrierWaves),
            waveOperations.CountMax(ref _rightBarrierWaves));
    }
}
