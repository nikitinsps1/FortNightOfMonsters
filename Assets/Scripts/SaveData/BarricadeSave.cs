
public class BarricadeSave
{
    public int[] Levels
    { get; private set; }

    public int[] HealthBarricade = new int[]{ 5, 10, 20};

    public BarricadeSave(int amountBarricades)
    {
        Levels = new int[amountBarricades];
    }

    public BarricadeSave (int[] barricadesLevels)
    {
        Levels = barricadesLevels;
    }

    public void OnUpgradeBarricade(int numberBarricade)
    {
        Levels[numberBarricade]++;
    }
}
