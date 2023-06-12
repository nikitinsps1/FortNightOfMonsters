using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BuyWeapon", menuName = "Level/Dialog/Create new Buy Weapon")]
public class DialogBuyWeapon : DialogAction
{
    [SerializeField]
    private int _cost;

    [SerializeField]
    private TypeWeapons _weapon;

    public override Action GetAction(DialogActionMediator mediator)
    {
        IsHaveNewTask = false;

        Action upgrade = delegate
        {
            mediator.SaveData.Weapons.Save((int)_weapon);
            mediator.Weapons.Upgraders[((int)_weapon)].Upgrade();
        };

        return delegate
        { mediator.BuyMenu.Trade(_cost, upgrade); };
    }
}