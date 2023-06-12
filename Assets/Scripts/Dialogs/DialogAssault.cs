using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BanditAssault", menuName = "Level/Dialog/Create new Assault")]
public class DialogAssault : DialogAction
{
    [SerializeField]
    private Directions _direction;

    [SerializeField]
    private WaveEnemies[] _waveEnemies;

    [SerializeField]
    private string _textTask;

    public override Action GetAction(DialogActionMediator mediator)
    {
        IsHaveNewTask = true;

        for (int i = 0; i < _waveEnemies.Length; i++)
        {
            _waveEnemies[i].Init();
        }

        WaveOperations waveOperations = new WaveOperations();
        WaveEnemies maxEnemies = new WaveEnemies();

        maxEnemies.ConstructEmptyWave();
        maxEnemies = waveOperations.CountMax(ref _waveEnemies);

        foreach (var item in maxEnemies.Amount)
        {
            mediator.EnemiesContainer
                .Pools[item.Key]
                .FormPool(item.Value);
        }

        return delegate { mediator.LevelProgress.SetNewTask(_textTask, _direction, _waveEnemies); };
    }
}