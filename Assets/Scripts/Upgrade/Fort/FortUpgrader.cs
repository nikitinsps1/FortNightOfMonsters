using UnityEngine;

public class FortUpgrader : Upgrader
{
    [field: SerializeField]
    public TypeFortUpgrade Type { get; private set; }


    [SerializeField]
    private GameObject[] _buildings;

    public override void Upgrade()
    {
        base.Upgrade();

        for (int i = 0; i < _buildings.Length; i++)
        {
            _buildings[i].SetActive(true);
        }
    }
}