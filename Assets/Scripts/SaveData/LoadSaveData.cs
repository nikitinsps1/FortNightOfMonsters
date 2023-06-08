using UnityEngine;
using Zenject;

public class LoadSaveData : MonoBehaviour
{
    [SerializeField]
    private LineDefense
        _left,
        _right;

    [SerializeField]
    private GuardsContainer _guards;

    [SerializeField]
    private BarricadeContainer _barricades;

    private ChangeWeaponPanel _changeWeapon;
    private SaveData _saveData;
    private Player _player;
    private MainHouse _mainHouse;

    [SerializeField]
    private HudBars _hud;

    [Inject]
    private void Construct(
        SaveData saveData,
        Player player,
        MainHouse mainHouse,
        ChangeWeaponPanel changeWeaponPanel)
    {
        _changeWeapon = changeWeaponPanel;
        _saveData = saveData;
        _player = player;
        _mainHouse = mainHouse;
    }

    private void Player()
    {
        float healthValue = _saveData
            .Characteristics.Levels
            [(int)TypeCharacteristicks.Health];

        _player.ThisDamageable.SetHealth(healthValue);
    }

    private void Barricade()
    {
        for (int i = 0; i < _saveData.Barricades.Levels.Length; i++)
        {
            _barricades.Upgrade(_barricades.Barricades[i], 
                    _saveData.Barricades.Levels[i], 
                    _saveData.Barricades.HealthBarricade[i]);
        }
    }

    private void Guards()
    {
        for (int i = 0; i < _saveData.Guards.RankLevels.Length; i++)
        {
            if (_saveData.Guards.RankLevels[i] > 0)
            {
                _guards.AddRankGuard(_guards.Guards[i], _saveData.Guards.RankLevels[i]);
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
        foreach (var building in _saveData.BaseUpgrade.Upgrades)
        {
            if (building.Value == true)
            {
                _mainHouse.Upgrade((TypeUpgradesBuildings)building.Key);
            }
        }
    }

    public void Load()
    {
        Player();
        Barricade();
        Guards();
        Weapons();
        MainBase();

        _hud.HealthBar();
        _hud.MoneyCounter();
    }
}