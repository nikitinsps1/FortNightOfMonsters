using TMPro;
using UnityEngine;
using Zenject;

public class UpgradeCharacteristics : Upgrade
{
    [SerializeField]
    private TypeCharacteristicks _type;

    [SerializeField]
    private TextMeshProUGUI _countCharacteristic;

    private Damageable _playerDamageable;

    [Inject]
    private void Construct(Player player)
    {
        _playerDamageable = player.ThisDamageable;
    }

    private void UpgradeHealth()
    {
        float value = SaveData.Characteristics
            .Levels[(int)_type];

        _playerDamageable.SetHealth(value);
    }

    protected override void OnEnable()
    {
        OnUpgrade += delegate
        {
            SaveData.Characteristics
            .UpCharacteristics(_type);
        };

        if (_type == TypeCharacteristicks.Health)
        {

            OnUpgrade += UpgradeHealth;
        }

        base.OnEnable();
    }

    protected override void RefreshPanel()
    {
        float value = SaveData.Characteristics
            .Levels[(int)_type];

        _countCharacteristic.text = value.ToString();

        if (_type == TypeCharacteristicks.Health)
        {
            LevelUpgrade =
                SaveData.Characteristics.HealthLevel;
        }
        else
        {
            LevelUpgrade = (int)value;
        }

        base.RefreshPanel();
    }
}