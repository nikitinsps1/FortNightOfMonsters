using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public abstract class Upgrade : MonoBehaviour
{
    [SerializeField]
    protected Button _button;

    [SerializeField]
    private int[] _costs;

    private BuyMenu _buyMenu;

    protected SaveData SaveData
    { get; private set; }

    protected event Action OnUpgrade;

    protected int LevelUpgrade;
    protected int MaxLevel => _costs.Length;

    [Inject]
    private void Construct(BuyMenu buyMenu, SaveData saveData)
    {
        _buyMenu = buyMenu;
        SaveData = saveData;
    }

    private void Start() => RefreshPanel();

    protected void GoMagazine()
    {
        _buyMenu.
            StartTrade(_costs[LevelUpgrade], OnUpgrade);
    }

    protected virtual void OnEnable()
    {
        _button.onClick.AddListener(GoMagazine);
        OnUpgrade += RefreshPanel;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
        OnUpgrade = null;
    }

    protected virtual void RefreshPanel()
    {
        if (LevelUpgrade == _costs.Length)
        {
            _button.interactable = false;
        }
    }
}