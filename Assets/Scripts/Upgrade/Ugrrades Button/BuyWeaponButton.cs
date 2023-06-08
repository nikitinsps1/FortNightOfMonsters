using TMPro;
using UnityEngine;

public class BuyWeaponButton : Upgrade
{
    [SerializeField]
    private TypeWeapons weapon;

    [SerializeField] 
    private ChangeWeaponPanel _changeWeapon;

    [SerializeField]
    private TextMeshProUGUI _textMeshProUGUI;

    protected override void RefreshPanel()
    {
        SaveData
            .Armoury.Weapons
            .TryGetValue((int)weapon, out bool have);

        if (have)
        {
            LevelUpgrade = 1;
            _textMeshProUGUI.text = "Продано!";
        }
        base.RefreshPanel();
    }

    protected override void OnEnable()
    {
        OnUpgrade += delegate
        { SaveData.Armoury.Add(weapon); };

        OnUpgrade += delegate 
        { _changeWeapon.OnBuyWeapon(weapon); };

        base.OnEnable();
    }
}