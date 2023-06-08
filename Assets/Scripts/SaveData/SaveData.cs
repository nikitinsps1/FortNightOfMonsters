using System;
using UnityEngine;
using YG;

public class SaveData : MonoBehaviour
{
    public CharacteristicsSave Characteristics
    { get; private set; }
    public GuardsSave Guards
    { get; private set; }
    public PlayerArsenalSave Armoury
    { get; private set; }
    public BaseUpgradeSave BaseUpgrade
    { get; private set; }
    public BarricadeSave Barricades
    { get; private set; }
    public int NumberLevel
    { get; private set; }
    public int Money
    { get; private set; }

    public event Action OnChangeMoney;

    private int _maxAmountGuards = 4;
    private int _amountBarricades = 2;
    private int _startMoney = 50;

    private float _startHealth = 10;

    public void NewGame( )
    {
        NumberLevel = 0;
        Guards = new GuardsSave(_maxAmountGuards);
        Characteristics = new CharacteristicsSave(_startHealth);
        Barricades = new BarricadeSave(_amountBarricades);
        Armoury = new PlayerArsenalSave();
        BaseUpgrade = new BaseUpgradeSave();
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
            new CharacteristicsSave (
                YandexGame.savesData.Health, 
                YandexGame.savesData.Charisma);

        Armoury = 
            new PlayerArsenalSave(
                YandexGame.savesData.ShootGun, 
                YandexGame.savesData.Riffle, 
                YandexGame.savesData.FlameThrower);

        BaseUpgrade = 
            new BaseUpgradeSave(
                YandexGame.savesData.LiveHouse, 
                YandexGame.savesData.Dynamite, 
                YandexGame.savesData.DefenseMainHouse);

        Guards =
            new GuardsSave(
                YandexGame.savesData.AmountGuards, 
                YandexGame.savesData.RanksGuards);

        Barricades =
            new BarricadeSave
            (YandexGame.savesData.BarricadesLevels);
    }

    public void SendServer()
    {
        YandexGame.savesData.ConvertGameData(this);
    }
}