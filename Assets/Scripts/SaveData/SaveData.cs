using System;
using UnityEngine;
using YG;

public class SaveData : MonoBehaviour
{
    public CharacteristicsSave Characteristics
    { get; private set; }
    public BarriersSave Guards
    { get; private set; }
    public BarriersSave Barricades
    { get; private set; }
    public PlayerArsenalSave Weapons
    { get; private set; }
    public FortUpgradeSave Fort
    { get; private set; }

    public int NumberLevel
    { get; private set; }
    public int Money
    { get; private set; }

    public event Action OnChangeMoney;

    private int _maxAmountGuards = 4;
    private int _amountBarricades = 2;
    private int _startMoney = 50;


    public void NewGame()
    {
        NumberLevel = 0;
        Characteristics = new CharacteristicsSave();
        Guards = new BarriersSave(_maxAmountGuards);
        Barricades = new BarriersSave(_amountBarricades);
        Weapons = new PlayerArsenalSave();
        Fort = new FortUpgradeSave();
        Money = _startMoney;
    }

    public void LevelComplete(int reward)
    {
        Money += reward;
        NumberLevel++;
    }

    public void RefreshAmountMoney(int addValue)
    {
        Money += addValue;
        OnChangeMoney.Invoke();
    }

    public void ConvertYandexData()
    {
        NumberLevel = YandexGame.savesData.Level;

        Money = YandexGame.savesData.Money;

        Characteristics =
            new CharacteristicsSave(
                YandexGame.savesData.Health,
                YandexGame.savesData.Charisma);

        Weapons =
            new PlayerArsenalSave(
                YandexGame.savesData.ShootGun,
                YandexGame.savesData.Riffle,
                YandexGame.savesData.FlameThrower);

        Fort =
            new FortUpgradeSave(
                YandexGame.savesData.LiveHouse,
                YandexGame.savesData.Dynamite,
                YandexGame.savesData.DefenseMainHouse);

        Guards =
            new BarriersSave(_maxAmountGuards, YandexGame.savesData.RanksGuards);

        Barricades =
            new BarriersSave
            (_amountBarricades, YandexGame.savesData.BarricadesLevels);
    }

    public void SendServer()
    {
        YandexGame.savesData.ConvertGameData(this);
    }
}