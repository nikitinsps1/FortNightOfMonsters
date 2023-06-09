using UnityEngine;

public class DefenseBagsUpgrader : FortUpgrader
{
    [SerializeField]
    private Damageable _mainHouse;

    [SerializeField]
    private float _addHealth;

    public override void Upgrade()
    {
        base.Upgrade();
        _mainHouse.SetHealth(_addHealth);
    }
}
