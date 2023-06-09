using TMPro;
using UnityEngine;

public class BuyWeaponPanel : UpgradePanel<WeaponProduct>
{
    [SerializeField] 
    private WeaponUpgraderContainer _changeWeapon;

    [SerializeField]
    private TextMeshProUGUI _textMeshProUGUI;

    protected override void RefreshPanel()
    {
        if (Upgrading.AllDone())
        {
            _textMeshProUGUI.text = "Продано!";
        }
        base.RefreshPanel();
    }

    protected override void SavePurchase()
    {
        SaveData.Weapons.Save((int)Upgrading.Type);
    }
}