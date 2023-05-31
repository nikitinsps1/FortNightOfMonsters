using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class SaveData : MonoBehaviour
{
    public Characteristicks Characteristick
    { get; private set; }
    public PlayerArsenalSave Armoury
    { get; private set; }
    public BaseUpgradeSave BaseUpgrade
    { get; private set; }
    public int NumberLevel
    { get; private set; }
    public int Money
    { get; private set; }

    public Dictionary<int, BarriersSave> BarriersUpgrades
                { get; private set; }

    public event Action OnChangeMoney;

    public void NewGame( )
    {
        NumberLevel = 0;
        Characteristick = new Characteristicks(10);
        Armoury = new PlayerArsenalSave();
        BaseUpgrade = new BaseUpgradeSave();

        BarriersUpgrades = new Dictionary<int, BarriersSave>
        {
            {((int) Directions.LeftFlank), new BarriersSave()},
            {((int) Directions.RightFlank), new BarriersSave()}
        };

        Money = 50;

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

        Characteristick = new Characteristicks
            (YandexGame.savesData.Health, YandexGame.savesData.Charisma);

        Armoury = new PlayerArsenalSave
        (YandexGame.savesData.Shootgun, YandexGame.savesData.Riffle, YandexGame.savesData.Flamebrower);

        BaseUpgrade = new BaseUpgradeSave
        (YandexGame.savesData.LiveHouse, YandexGame.savesData.Dynamite, YandexGame.savesData.DefenceMainHouse);


        BarriersUpgrades = new Dictionary<int, BarriersSave>
            {
                {((int) Directions.LeftFlank), new BarriersSave
                (YandexGame.savesData.FirstBarricade, YandexGame.savesData.FirstFirstGuard,
                YandexGame.savesData.FirstSecondGuard, YandexGame.savesData.FirstAmountGuard)},

            {((int) Directions.RightFlank), new BarriersSave
                (YandexGame.savesData.SecondBarricade, YandexGame.savesData.SecondBFirstGuard,
                YandexGame.savesData.SecondBSecondGuard, YandexGame.savesData.SecondBAmountGuard)},
        };
    }

    public void SendServer()
    {

        YandexGame.savesData.ConvertGameData(this);
    }
}