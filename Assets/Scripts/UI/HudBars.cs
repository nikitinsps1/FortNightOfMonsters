using TMPro;
using UnityEngine;
using Zenject;

public class HudBars : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI
        _health,
        _money;

    private Damageable _player;
    private SaveData _saveData;

    [Inject]
    private void Construct(SaveData saveData, PlayerHeroLogic player)
    {
        _player = player.ThisDamageable;
        _saveData = saveData;
    }

    private void Start()
    {
        HealthBar();
        MoneyCounter();
    }

    private void OnEnable()
    {
        _player.OnSetHealth += HealthBar;
        _player.OnApplyDamage += HealthBar;
        _saveData.OnChangeMoney += MoneyCounter;
    }

    private void OnDisable()
    {
        _player.OnSetHealth -= HealthBar;
        _player.OnApplyDamage -= HealthBar;
        _saveData.OnChangeMoney -= MoneyCounter;
    }

    public void HealthBar()
    {
        _health.text = _player.Health.ToString();
    }

    public void MoneyCounter()
    {
        _money.text = _saveData.Money.ToString();
    }
}