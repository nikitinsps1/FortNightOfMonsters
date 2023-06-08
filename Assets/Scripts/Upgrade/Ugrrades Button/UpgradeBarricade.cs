using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBarricade : Upgrade
{
    [SerializeField]
    private BarricadeContainer _barricadesContainer;

    [SerializeField]
    private Barricade _upgradingBarricades;

    [SerializeField]
    private Image _image;

    [SerializeField]
    private Sprite[] _typeUpgradesImages;

    private int _indexBarricade;

    private void Awake()
    {
        Init();
    }

    private void ChangeImage()
    {
        _image.sprite =
            _typeUpgradesImages[LevelUpgrade];
    }

    private void Init()
    {
        if (_barricadesContainer.Barricades.Contains(_upgradingBarricades))
        {
            _indexBarricade =
            Array.IndexOf(_barricadesContainer.Barricades, _upgradingBarricades);
        }
        else
        {
            Debug.Log("Barricades not find of container");
        }
    }

    protected override void OnEnable()
    {
        OnUpgrade += delegate
        {
            SaveData.Barricades.OnUpgradeBarricade(_indexBarricade);
            _barricadesContainer.Upgrade(
                _upgradingBarricades,
                SaveData.Barricades.Levels[_indexBarricade],
                SaveData.Barricades.HealthBarricade[_indexBarricade]);
        };

        base.OnEnable();
    }

    protected override void RefreshPanel()
    {
        LevelUpgrade = SaveData.Barricades.Levels[_indexBarricade];
        ChangeImage();
        base.RefreshPanel();
    }
}
