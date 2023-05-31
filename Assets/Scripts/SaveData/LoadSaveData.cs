using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LoadSaveData : MonoBehaviour
{
    [SerializeField]
    private Barrier
        _left,
        _right;

    private ChangeWeaponPanel _changeWeapon;
    private SaveData _saveData;
    private Player _player;
    private MainHouse _mainHouse;

    [SerializeField]
    private HudBars _hud;

    [Inject]
    private void Construct(
        SaveData saveData
        ,Player player
        ,MainHouse mainHouse
        ,ChangeWeaponPanel changeWeaponButtons)
    {
        _changeWeapon = changeWeaponButtons;
        _saveData = saveData;
        _player = player;
        _mainHouse = mainHouse;

    }

    private void Player()
    {
        float healthValue = _saveData
            .Characteristick.Dictionary
            [((int)TypeCharacteristicks.Health)];

        _player.ThisDamageable.SetHealth(healthValue);
    }

    private void Barriers()
    {
        Dictionary<int, Barrier> barriers =
            new Dictionary<int, Barrier>
        {
            {((int) Directions.LeftFlank), _left},
            {((int) Directions.RightFlank), _right }
        }
        ;

        foreach (var barrier in barriers)
        {
            BarriersSave save = 
                _saveData.BarriersUpgrades[barrier.Key];

            int upBarricade = save.BarricadeLevel;
            int upFirstGuard = save.GuardsLevel[0];
            int upSecondGuard = save.GuardsLevel[1];

            barrier.Value.UpgradeBarricade
                (upBarricade, save.HealthBarricade[upBarricade]);

            if (upFirstGuard>0)
            {
                barrier.Value.AddRankGuard(0, upFirstGuard);
            }

            if (upSecondGuard > 0)
            {
                barrier.Value.AddRankGuard(1, upSecondGuard);
            }
        }
    }

    private void Weapons()
    {
        foreach (var weapon in _saveData.Armoury.Weapons)
        {
            if (weapon.Value == true)
            {
                _changeWeapon.OnBuyWeapon((TypeWeapons)weapon.Key);
            }
        }
    }

    private void MainBase()
    {
        foreach (var building in _saveData.BaseUpgrade.Upgrade)
        {
            if (building.Value == true)
            {
                _mainHouse.Upgrade((TypeUpgradesBuildings)building.Key);
            }
        }
    }

    public void Init()
    {
        Player();
        Barriers();
        Weapons();
        MainBase();

        _hud.HealthBar();
        _hud.MoneyCounter();
    }
}