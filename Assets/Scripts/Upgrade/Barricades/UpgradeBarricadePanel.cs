using UnityEngine;
using UnityEngine.UI;

public class UpgradeBarricadePanel : UpgradePanel<UpgraderBarricade>
{
    [SerializeField]
    private UpgradersBarrierContainer _container;

    [SerializeField]
    private Image _image;

    [SerializeField]
    private Sprite[] _typeUpgradesImages;

    protected override void RefreshPanel()
    {
        _image.sprite =
            _typeUpgradesImages[Upgrading.Level];

        base.RefreshPanel();
    }

    protected override void SavePurchase()
    {
        SaveData.Barricades.Save(_container.GetIndex(Upgrading));
    }
}
