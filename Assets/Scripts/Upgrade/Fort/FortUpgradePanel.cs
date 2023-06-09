
public class FortUpgradePanel : UpgradePanel<FortUpgrader>
{
    protected override void SavePurchase()
    {
        SaveData.Fort.Save
            ((int)Upgrading.Type);
    }
}
