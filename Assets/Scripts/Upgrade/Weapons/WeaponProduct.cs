using UnityEngine;

public class WeaponProduct : Upgrader
{
    [field: SerializeField]
    public TypeWeapons Type { get; private set; }

    [SerializeField]
    private WeaponButton _weaponButton;

    public override void Upgrade()
    {
        base.Upgrade();
        _weaponButton.ThisButton.interactable = true;
    }
}