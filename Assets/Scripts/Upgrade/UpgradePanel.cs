using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public abstract class UpgradePanel<Upgrade> : MonoBehaviour where Upgrade : Upgrader
{
    [SerializeField]
    protected Button _button;

    [SerializeField]
    protected Upgrade _upgrading;

    private BuyMenu _buyMenu;

    protected Action OnPurchase;
    protected Upgrade Upgrading => _upgrading;
    protected SaveData SaveData
    { get; private set; }

    [Inject]
    private void Construct(BuyMenu buyMenu, SaveData saveData)
    {
        _buyMenu = buyMenu;
        SaveData = saveData; 
    }

    private void Start() => RefreshPanel();

    private void OnEnable()
    {
        _button.onClick.AddListener(OpenBuyMenu);

        OnPurchase += Upgrading.Upgrade;
        OnPurchase += RefreshPanel;
        OnPurchase += SavePurchase;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OpenBuyMenu);

        OnPurchase -= Upgrading.Upgrade;
        OnPurchase -= RefreshPanel;
        OnPurchase -= SavePurchase;
    }

    protected virtual void OpenBuyMenu()
    {
        _buyMenu.Trade(Upgrading.GetCost(), OnPurchase);
    }

    protected virtual void RefreshPanel()
    {
        if (Upgrading.AllDone())
        {
            _button.interactable = false;
        }
    }

    protected abstract void SavePurchase();
}