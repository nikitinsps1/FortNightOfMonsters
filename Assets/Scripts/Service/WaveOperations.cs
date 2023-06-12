public class WaveOperations
{
    public WaveEnemies CountMax(ref WaveEnemies[] waves)
    {
        WaveEnemies result = new WaveEnemies();
        result.ConstructEmptyWave();

        for (int i = 0; i < waves.Length; i++)
        {
            foreach (var enemyType in waves[i].Amount)
            {
                if (enemyType.Value > result.Amount[enemyType.Key])
                {
                    result.Amount[enemyType.Key] = enemyType.Value;
                }
            }
        }

        return result;
    }

    public WaveEnemies SumWaves(WaveEnemies first, WaveEnemies second)
    {
        WaveEnemies result = new WaveEnemies();
        WaveEnemies filledIn = new WaveEnemies();
        result.ConstructEmptyWave();
        filledIn.ConstructEmptyWave();

        foreach (var item in result.Amount)
        {
            int amountEnemy =
                first.Amount[item.Key] + second.Amount[item.Key];

            filledIn.Amount[item.Key] = amountEnemy;
        }
        result = filledIn;
        return result;
    }
}