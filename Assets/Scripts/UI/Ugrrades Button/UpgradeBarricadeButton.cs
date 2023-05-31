using UnityEngine;
using UnityEngine.UI;

public class UpgradeBarricadeButton : UpgradeButton
{
    [SerializeField] 
    private Directions _directions;

    [SerializeField]
    private Barrier _defense;

    [SerializeField]
    private Image _image;

    [SerializeField] 
    private Sprite[] _typeUpgradesImages;

    private BarriersSave _save;

    protected override void Init()
    {
        base.Init();

        SaveData
            .BarriersUpgrades
            .TryGetValue
            (((int)_directions), out BarriersSave save);

        _save = save;
    }

    protected override void OnEnable()
    {
        OnUpgrade += _save.OnUpgradeBarricade;

        OnUpgrade += delegate
        { 
            _defense.UpgradeBarricade
            (_save.BarricadeLevel
            ,_save.HealthBarricade[_save.BarricadeLevel]); 
        };

        base.OnEnable();
    }

    protected override void RefreshPanel()
    {
        LevelUpgrade = _save.BarricadeLevel;
        ChangeImage();
        base.RefreshPanel();
    }

    private void ChangeImage()
    {
        _image.sprite = 
            _typeUpgradesImages[LevelUpgrade];
    }
}
