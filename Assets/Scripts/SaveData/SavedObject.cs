using System.Collections.Generic;

public abstract class SavedObject
{
    public Dictionary<int, int> ThisDictionary
    { get; protected set; }

    public void Save(int index)
    {
        ThisDictionary[index]++;
    }
}
