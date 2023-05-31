using UnityEngine;
using UnityEngine.UI;
public enum NumberGuard
{
    FirstGuard = 0,
    SecondGuard = 1
}

public class RecruitButton : UpgradeButton
{
    [SerializeField]
    private Directions _directions;

    [SerializeField]
    private NumberGuard _numberGuard;

    [SerializeField]
    private Barrier _defense;

    [SerializeField]
    private Image _image;

    [SerializeField]
    private Sprite[] _typeUpgradesImages;

    [SerializeField]
    private InfoPanel _infoPanel;

    private BarriersSave _barrierSave;

    protected override void Init()
    {
        base.Init();

        SaveData
            .BarriersUpgrades
            .TryGetValue
            (((int)_directions), out BarriersSave container);

        _barrierSave = container;
    }

    protected override void OnEnable()
    {
        OnUpgrade += Recruit;
        OnUpgrade += RefreshPanel;
        _button.onClick.AddListener(Check);
    }

    private void Check()
    {
        bool IsBuildLiveHouse =
            SaveData
            .BaseUpgrade
            .Upgrade
            [((int)TypeUpgradesBuildings.LiveHouse)];

        if (_barrierSave.AmountGuard == 0 || IsBuildLiveHouse || LevelUpgrade > 0)
        {
            GoMagazine();
        }
        else
        {
            _infoPanel.ShowMessage
                ("Для найма дополнительного охранника, постройте жилой дом");
        }
    }

    private void Recruit()
    {
        int numberGuard =((int)_numberGuard);

        _barrierSave.OnUpGuard(numberGuard);

        _defense.AddRankGuard
           (numberGuard, _barrierSave.GuardsLevel[numberGuard]);
    }

    protected override void RefreshPanel()
    {
        LevelUpgrade = 
            _barrierSave.GuardsLevel[((int)_numberGuard)];

        ChangeImage();

        base.RefreshPanel();
    }

    private void ChangeImage()
    {
        _image.sprite = _typeUpgradesImages[LevelUpgrade];
    }
}