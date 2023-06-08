using System.Collections.Generic;

public class PlayerArsenalSave
{
    public Dictionary<int, bool> Weapons 
    { get; private set; }
 
    public PlayerArsenalSave
        (bool shootGun = false
        ,bool riffle = false 
        ,bool flamethrower = false)
    {
        Weapons = new Dictionary<int, bool>
        {
            {(int) TypeWeapons.ShootGun, shootGun },
            {(int) TypeWeapons.Riffle, riffle },
            {(int) TypeWeapons.Flamethrower, flamethrower }
        };
    }
    public void Add(TypeWeapons type)
    {
        Weapons[(int)type] = true;
    }
}
