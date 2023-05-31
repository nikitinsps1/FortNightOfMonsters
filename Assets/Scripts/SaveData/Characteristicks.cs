using System.Collections.Generic;

public class Characteristicks
{
    public Characteristicks(float health, float charisma = 0)
    {
        Dictionary = new Dictionary<int, float>
        {
            { ((int)TypeCharacteristicks.Health),  health },
            { ((int)TypeCharacteristicks.Charisma), charisma }
        };
    }

    public Dictionary<int, float>  Dictionary;

    public void UpCharacterictick(TypeCharacteristicks type)
    {
        if (type == TypeCharacteristicks.Health)
        {
            HealthLevel++;
            Dictionary[((int)type)] += 10;
        }
        else
        {
            Dictionary[((int)type)] += 1;
        }
    }

    public int HealthLevel { get; private set; }
}
