using System.Collections.Generic;

public class CharacteristicsSave
{
    public CharacteristicsSave(float health, float charisma = 0)
    {
        Levels = new Dictionary<int, float>
        {
            {(int)TypeCharacteristicks.Health,  health },
            {(int)TypeCharacteristicks.Charisma, charisma }
        };
    }

    public Dictionary<int, float> Levels;

    public void UpCharacteristics(TypeCharacteristicks type)
    {
        if (type == TypeCharacteristicks.Health)
        {
            HealthLevel++;
            Levels[(int)type] += 10;
        }
        else
        {
            Levels[(int)type] += 1;
        }
    }

    public int HealthLevel { get; private set; }
}