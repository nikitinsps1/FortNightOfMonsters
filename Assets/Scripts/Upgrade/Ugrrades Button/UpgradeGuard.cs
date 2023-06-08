using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UpgradeGuard : Upgrade
{
    [SerializeField]
    private GuardsContainer _guardsContainer;

    [SerializeField]
    private Image _image;

    [SerializeField]
    private Guard _upgradingGuard;

    [SerializeField]
    private Sprite[] _typeUpgradesImages;

    private InfoPanel _infoPanel;

    private int _indexGuard;

    [Inject]
    private void Construct(InfoPanel infoPanel)
    {
        _infoPanel = infoPanel;
    }

    private void Awake()
    {
        Init();
    }

    private void AddRank()
    {
        SaveData.Guards.OnUpgrade(_indexGuard);

        _guardsContainer.AddRankGuard
            (_upgradingGuard, SaveData.Guards.RankLevels[_indexGuard]);
    }

    private void ChangeImage()
    {
        _image.sprite = _typeUpgradesImages[LevelUpgrade];
    }

    private void Check()
    {
        bool IsBuildLiveHouse =
            SaveData.BaseUpgrade.Upgrades
            [(int)TypeUpgradesBuildings.LiveHouse];

        if (SaveData.Guards.AmountGuards < 2 || IsBuildLiveHouse || LevelUpgrade > 0)
        {
            GoMagazine();
        }
        else
        {
            _infoPanel.ShowMessage
                ("Для найма дополнительного охранника, постройте жилой дом");
        }
    }

    private void Init()
    {
        if (_guardsContainer.Guards.Contains(_upgradingGuard))
        {
            _indexGuard =
            Array.IndexOf(_guardsContainer.Guards, _upgradingGuard);
        }
        else
        {
            Debug.Log("Guard not find of container");
        }

    }

    protected override void OnEnable()
    {
        OnUpgrade += AddRank;
        OnUpgrade += RefreshPanel;
        _button.onClick.AddListener(Check);
    }


    protected override void RefreshPanel()
    {
        LevelUpgrade =
            SaveData.Guards.RankLevels[_indexGuard];

        ChangeImage();
        base.RefreshPanel();
    }
}