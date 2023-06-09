using UnityEngine;
using UnityEngine.UI;

public class UpgradeGuardPanel : UpgradePanel<GuardUpgrade>
{
    [SerializeField]
    private Image _image;

    [SerializeField]
    private Sprite[] _typeUpgradesImages;

    [SerializeField]
    private InfoPanel _infoPanel;

    [SerializeField]
    private UpgradersBarrierContainer _container;

    private int GetAmountGuards()
    {
        int amountGuard = 0;

        foreach (var item in _container.Upgraders)
        {
            if (item.Value.Level>0)
            {
                amountGuard++;
            }
        }
        return amountGuard;
    }


    protected override void OpenBuyMenu()
    {
        int amountGuard = GetAmountGuards();
        int amountLiveHouse = SaveData.Fort.ThisDictionary[(int)TypeFortUpgrade.LiveHouse];

        if (amountGuard < 2 || amountLiveHouse>0 || Upgrading.Level > 0)
        {
            base.OpenBuyMenu();
        }
        else
        {
            _infoPanel.ShowMessage
                ("Для найма дополнительного охранника, постройте жилой дом");
        }

    }

    protected override void RefreshPanel()
    {
        _image.sprite = _typeUpgradesImages[Upgrading.Level];
        base.RefreshPanel();
    }

    protected override void SavePurchase()
    {
        int index = _container.GetIndex(Upgrading);

        SaveData.Guards.Save(index);
    }
}