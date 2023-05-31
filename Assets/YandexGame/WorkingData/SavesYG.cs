
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;


        ////////Base
        public int Level;
        public int Money;

        ////////Characteristick
        public float Health;
        public float Charisma;

        ////////Weapon
        public bool Shootgun;
        public bool Riffle;
        public bool Flamebrower;

        ////////Buidling
        public bool LiveHouse;
        public bool Dynamite;
        public bool DefenceMainHouse;

        ////////Barriers 1
        public int FirstBarricade;
        public int FirstAmountGuard;
        public int FirstFirstGuard;
        public int FirstSecondGuard;


        ////////Barriers 2
        public int SecondBarricade;
        public int SecondBAmountGuard;
        public int SecondBFirstGuard;
        public int SecondBSecondGuard;


        public void ConvertGameData(SaveData saveData)
        {

            Money = saveData.Money;
            Level = saveData.NumberLevel;


            Health = saveData.Characteristick.Dictionary[((int)TypeCharacteristicks.Health)];
            Charisma = saveData.Characteristick.Dictionary[((int)TypeCharacteristicks.Charisma)];

            Shootgun = saveData.Armoury.Weapons[((int)TypeWeapons.ShootGun)];
            Riffle = saveData.Armoury.Weapons[((int)TypeWeapons.Riffle)];
            Flamebrower = saveData.Armoury.Weapons[((int)TypeWeapons.FlameBrower)];


            DefenceMainHouse = saveData.BaseUpgrade.Upgrade[((int)TypeUpgradesBuildings.MainHouseDefense)];
            Dynamite = saveData.BaseUpgrade.Upgrade[((int)TypeUpgradesBuildings.Dynamite)];
            LiveHouse = saveData.BaseUpgrade.Upgrade[((int)TypeUpgradesBuildings.LiveHouse)];

            BarriersSave leftSave = saveData.BarriersUpgrades[((int)Directions.LeftFlank)];
            BarriersSave rightSave = saveData.BarriersUpgrades[((int)Directions.RightFlank)];

            FirstBarricade = leftSave.BarricadeLevel;
            FirstAmountGuard = leftSave.AmountGuard;
            FirstFirstGuard = leftSave.GuardsLevel[0];
            FirstSecondGuard = leftSave.GuardsLevel[1];

            SecondBarricade = rightSave.BarricadeLevel;
            SecondBAmountGuard = rightSave.AmountGuard;
            SecondBFirstGuard = rightSave.GuardsLevel[0];
            SecondBSecondGuard = rightSave.GuardsLevel[1];

            YandexGame.SaveProgress();
        }
        public SavesYG()
        {
        
        }
    }
}
