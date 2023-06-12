using UnityEngine;
using Zenject;

//////////////Данный класс - костыль. И знаю, что никаким медиатором не является. Думаю как заменить.

public class DialogActionMediator : MonoBehaviour
{
    private BuyMenu _buyMenu;
    private SaveData _saveData;
    private LevelProgress _levelProgress;
    private EnemiesContainer _enemiesContainer;
    private WeaponUpgraderContainer _weapons;

    public BuyMenu BuyMenu => _buyMenu;
    public SaveData SaveData => _saveData;
    public LevelProgress LevelProgress => _levelProgress;
    public EnemiesContainer EnemiesContainer => _enemiesContainer;
    public WeaponUpgraderContainer Weapons => _weapons;

    [Inject]
    private void Construct(
        BuyMenu buyMenu,
        SaveData saveData,
        LevelProgress progress,
        EnemiesContainer enemies,
        WeaponUpgraderContainer weapons)
    {
        _buyMenu = buyMenu;
        _saveData = saveData;
        _levelProgress = progress;
        _enemiesContainer = enemies;
        _weapons = weapons;
    }
}
