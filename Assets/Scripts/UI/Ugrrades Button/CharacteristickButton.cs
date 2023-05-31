using TMPro;
using UnityEngine;
using Zenject;

public class CharacteristickButton : UpgradeButton
{
    [SerializeField]
    private TypeCharacteristicks _type;

    [SerializeField]
    private TextMeshProUGUI _countCharacteristick;




    private Damageable _playerDamagable;

    [SerializeField]
    private HudBars _hudBars;
    [Inject]
    private void GetPlayer(Player player)
    {
        _playerDamagable = player.ThisDamageable;
    }

    protected override void OnEnable()
    {
        OnUpgrade += delegate
        {
            SaveData
            .Characteristick
            .UpCharacterictick(_type);
        };


        if (_type == TypeCharacteristicks.Health)
        {

            OnUpgrade += UpgradeHealth;


        }

        base.OnEnable();
    }


   private void UpgradeHealth()
    {
        float value =
               SaveData
               .Characteristick
               .Dictionary[((int)_type)];

        _playerDamagable.SetHealth(value);

    }


    protected override void RefreshPanel()
    {
        float value =
            SaveData
            .Characteristick
            .Dictionary[(int)_type];

        _countCharacteristick.text = value.ToString();

        if (_type == TypeCharacteristicks.Health)
        {
            LevelUpgrade =
                SaveData
                .Characteristick
                .HealthLevel;
        }
        else
        {
            LevelUpgrade = ((int)value);
        }

        base.RefreshPanel();
    }
}