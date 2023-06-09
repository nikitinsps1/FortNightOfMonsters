using System;
using UnityEngine;
using Zenject;

public class DialogBuyWeapon : DialogAction
{
    [SerializeField]
    private int _cost;

    [SerializeField]
    private WeaponProduct _weapon;

    private BuyMenu _magazine;
    private SaveData _saveData;

    [Inject]
    private void Construct(BuyMenu buyMenu, SaveData saveData)
    {
        _magazine = buyMenu;
        _saveData = saveData;
    }

    public override Action GetEvent()
    {
        HaveNewTask = false;

        Action upgrade = delegate
        {_saveData.Weapons.Save((int)_weapon.Type);};

         upgrade +=_weapon.Upgrade;

        Action purchase = delegate 
        {_magazine.Trade(_cost, upgrade);};

        return purchase;
    }
}