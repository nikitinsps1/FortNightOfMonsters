using System.Collections.Generic;

public class PlayerArsenalSave
{
    public Dictionary<int, bool> Weapons 
    { get; private set; }
 
    public PlayerArsenalSave(
        bool shootgun = false
        ,bool riffle = false 
        ,bool flamebrower = false)
    {
        Weapons = new Dictionary<int, bool>
        {
            {((int) TypeWeapons.ShootGun), shootgun },
            {((int) TypeWeapons.Riffle), riffle },
            {((int) TypeWeapons.FlameBrower), flamebrower }
        };
    }
    public void Add(TypeWeapons type)
    {
        Weapons[((int)type)] = true;
    }
}
