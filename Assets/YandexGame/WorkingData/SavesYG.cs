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

            Health,
            Charisma,

            ShootGun,
            Riffle,
            FlameThrower,

            LiveHouse,
            Dynamite,
            DefenseMainHouse;

        public int[]
            RanksGuards,
            BarricadesLevels;



        public void ConvertGameData(SaveData saveData)
        {
            Level = saveData.NumberLevel;
            Money = saveData.Money;

            RanksGuards = GetValuesDictionary(saveData.Guards.ThisDictionary);
            BarricadesLevels = GetValuesDictionary(saveData.Barricades.ThisDictionary);

            Characteristics(saveData.Characteristics.ThisDictionary);
            Building(saveData.Fort.ThisDictionary);
            Weapons(saveData.Weapons.ThisDictionary);

            YandexGame.SaveProgress();
        }

        public int[] GetValuesDictionary(Dictionary<int, int> dictionary)
        {
            int[] values = new int[dictionary.Count];

            foreach (var item in dictionary)
            {
                values[item.Key] = item.Value;
            }
            return values;
        }

        public void Characteristics(Dictionary<int, int> characteristics)
        {
            Health = characteristics
                [(int)TypeCharacteristicks.Health];

            Charisma = characteristics
                [(int)TypeCharacteristicks.Charisma];
        }

        public void Building(Dictionary<int, int> building)
        {
            DefenseMainHouse = building
                [(int)TypeFortUpgrade.DefenseBag];

            Dynamite = building
                [(int)TypeFortUpgrade.Dynamite];

            LiveHouse = building
                [(int)TypeFortUpgrade.LiveHouse];
        }

        public void Weapons(Dictionary<int, int> weapons)
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
