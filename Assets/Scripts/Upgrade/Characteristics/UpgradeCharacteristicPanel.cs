using TMPro;
using UnityEngine;

public class UpgradeCharacteristicPanel : UpgradePanel<CharacteristicsUpgrader>
{
    [SerializeField]
    private TextMeshProUGUI _countCharacteristic;

    protected override void RefreshPanel()
    {
        base.RefreshPanel();

        if (Upgrading.Level>0)
        {
            _countCharacteristic
                .text = Upgrading.GetValue().ToString();
        }
    }

    protected override void SavePurchase()
    {
      

        SaveData.Characteristics
            .Save((int)Upgrading.Type);
    }
}