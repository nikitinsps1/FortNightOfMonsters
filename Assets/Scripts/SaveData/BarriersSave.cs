using System.Collections.Generic;

public class BarriersSave : SavedObject
{
    public BarriersSave(int maxAmount)
    {
        ThisDictionary = new Dictionary<int, int>();

        for (int i = 0; i < maxAmount; i++)
        {
            ThisDictionary.Add(i, 0);
        }
    }

    public BarriersSave(int amount, int[] levels)
    {
        ThisDictionary = new Dictionary<int, int>();

        for (int i = 0; i < amount; i++)
        {
            ThisDictionary.Add(i, levels[i]);
        }
    }
}
