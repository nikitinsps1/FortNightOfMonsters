using System.Collections.Generic;

public class PlayerArsenalSave: SavedObject
{
    public PlayerArsenalSave(
         int shootGun = 0,
         int riffle = 0,
         int flamethrower = 0)
    {
        ThisDictionary = new Dictionary<int, int>
        {
            {(int) TypeWeapons.ShootGun, shootGun },
            {(int) TypeWeapons.Riffle, riffle },
            {(int) TypeWeapons.Flamethrower, flamethrower }
        };
    }
}
