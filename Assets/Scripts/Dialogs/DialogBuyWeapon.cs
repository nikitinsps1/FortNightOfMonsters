using System;
using UnityEngine;
using Zenject;

public class DialogBuyWeapon : DialogAction
{
    [SerializeField]
    private TypeWeapons _weapon;

    [SerializeField]
    private int _cost;

    private ChangeWeaponPanel _changeWeaponButtons;
    private BuyMenu _magazine;
    private SaveData _saveData;

    [Inject]
    private void Construct(BuyMenu buyMenu,  ChangeWeaponPanel changeWeaponButtons, SaveData saveData)
    {
        _changeWeaponButtons = changeWeaponButtons;
        _magazine = buyMenu;
        _saveData = saveData;
    }

    public override Action GetEvent()
    {
        HaveNewTask = false;

        Action upgrade = delegate
        { _saveData.Armoury.Add(_weapon); };

         upgrade += delegate
        { _changeWeaponButtons.OnBuyWeapon(_weapon); };

        Action purchase = delegate 
       { _magazine.StartTrade(_cost, upgrade); };

        return purchase;
    }
}