using System.Collections.Generic;

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
        public int
            Level,
            Money,
            AmountGuards;

        public int[]
            RanksGuards,
            BarricadesLevels;

        public float
            Health,
            Charisma;

        public bool
            ShootGun,
            Riffle,
            FlameThrower,

            LiveHouse,
            Dynamite,
            DefenseMainHouse;


        public void ConvertGameData(SaveData saveData)
        {
            Level = saveData.NumberLevel;
            Money = saveData.Money;

            AmountGuards = saveData.Guards.AmountGuards;
            RanksGuards = saveData.Guards.RankLevels;
            BarricadesLevels = saveData.Barricades.Levels;

            Characteristics(saveData.Characteristics.Levels);
            Building(saveData.BaseUpgrade.Upgrades);
            Weapons(saveData.Armoury.Weapons);

            YandexGame.SaveProgress();
        }

        public void Characteristics(Dictionary<int, float> characteristics)
        {
            Health = characteristics
                [(int)TypeCharacteristicks.Health];

            Charisma = characteristics
                [(int)TypeCharacteristicks.Charisma];
        }

        public void Building(Dictionary<int, bool> building)
        {
            DefenseMainHouse = building
                [(int)TypeUpgradesBuildings.MainHouseDefense];

            Dynamite = building
                [(int)TypeUpgradesBuildings.Dynamite];

            LiveHouse = building
                [(int)TypeUpgradesBuildings.LiveHouse];
        }

        public void Weapons(Dictionary<int, bool> weapons)
        {
            ShootGun = weapons
                [(int)TypeWeapons.ShootGun];

            Riffle = weapons
                [(int)TypeWeapons.Riffle];

            FlameThrower = weapons
                [(int)TypeWeapons.Flamethrower];
        }

        public SavesYG()
        {

        }
    }
}
