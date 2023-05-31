
public class BarriersSave
{
    public int BarricadeLevel 
    { get; private set; }

    public int[] GuardsLevel 
    { get; private set; }

    public int AmountGuard
    { get; private set; }

    public int[] HealthBarricade 
    { get; private set; }

    public BarriersSave
        (int barricadeLevel = 0
        ,int firstGuardLevel = 0
        ,int secondGuardLevel = 0
        ,int amountGuard = 0)
    {
        HealthBarricade =
            new int[] { 5, 10, 20};

        GuardsLevel =
            new int[] { firstGuardLevel, secondGuardLevel };

        BarricadeLevel = barricadeLevel;
        AmountGuard = amountGuard;
    }

    public void OnUpgradeBarricade()
    {
        BarricadeLevel++;
    }

    public void OnUpGuard( int numberGuard)
    {
        if (GuardsLevel[numberGuard] == 0)
        {
            AmountGuard++;
        }
        GuardsLevel[numberGuard]++;
    }
}
