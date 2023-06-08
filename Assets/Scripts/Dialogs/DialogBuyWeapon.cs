using System;
using UnityEngine;
using Zenject;

public class DialogBuyWeapon : DialogAction
{
    [SerializeField]
    private TypeWeapons _weapon;

    [SerializeField]
    private int _cost;

    private ChangeWeaponPanel _changeWeapon;
    private BuyMenu _magazine;
    private SaveData _saveData;

    [Inject]
    private void Construct(BuyMenu buyMenu,  ChangeWeaponPanel changeWeapon, SaveData saveData)
    {
        _changeWeapon = changeWeapon;
        _magazine = buyMenu;
        _saveData = saveData;
    }

    public override Action GetEvent()
    {
        HaveNewTask = false;

        Action upgrade = delegate
        {_saveData.Armoury.Add(_weapon);};

         upgrade += delegate
        {_changeWeapon.OnBuyWeapon(_weapon);};

        Action purchase = delegate 
        {_magazine.StartTrade(_cost, upgrade);};

        return purchase;
    }
}