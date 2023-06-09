using System.Collections.Generic;

public class CharacteristicsSave: SavedObject
{
    public CharacteristicsSave(int health = 0 , int charisma = 0)
    {
        ThisDictionary = new Dictionary<int, int>
        {
            {(int)TypeCharacteristicks.Health,  health },
            {(int)TypeCharacteristicks.Charisma, charisma }
        };
    }
}