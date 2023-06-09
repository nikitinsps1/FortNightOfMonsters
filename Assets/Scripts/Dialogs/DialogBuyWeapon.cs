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

    public override Action GetAction()
    {
        IsHaveNewTask = false;

        Action upgrade = delegate
        {
            _saveData.Weapons.Save((int)_weapon.Type);
            _weapon.Upgrade();
        };

        return delegate
        { _magazine.Trade(_cost, upgrade); };
    }
}