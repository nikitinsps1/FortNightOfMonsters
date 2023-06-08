
public class GuardsSave 
{
    public int[] RankLevels 
    { get; private set; }
    public int AmountGuards
    { get; private set;}

    public GuardsSave(int maxAmountGuards)
    {
        RankLevels = new int[maxAmountGuards];
    }

    public GuardsSave(int amountGuards, int[] ranksGuards)
    {
        RankLevels = ranksGuards;
        AmountGuards = amountGuards;
    }

    public void OnUpgrade(int numberGuard)
    {
        if (RankLevels[numberGuard] == 0)
        {
            AmountGuards++;
        }

        RankLevels[numberGuard]++;
    }
}
