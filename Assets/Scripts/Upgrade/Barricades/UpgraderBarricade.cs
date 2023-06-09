using UnityEngine;

public class UpgraderBarricade : Upgrader
{
    [SerializeField]
    private Damageable _damageableBarricade;

    [SerializeField]
    private GameObject _defaultMeshes;

    [SerializeField]
    private GameObject[] _upgradesMeshes;

    [SerializeField]
    private int[] _healthValues;

    private void OnValidate()
    {
        if (MaxLevel != _healthValues.Length)
        {
            _healthValues = new int[MaxLevel];
            Debug.Log("Количество улучшений приравнено к максимальному уровню");
        }

        if (MaxLevel != _upgradesMeshes.Length)
        {
            _upgradesMeshes = new GameObject[MaxLevel];
            Debug.Log("Количество улучшений приравнено к максимальному уровню");
        }
    }

    public override void Upgrade()
    {
        _defaultMeshes.SetActive(false);

        for (int i = 0; i < _upgradesMeshes.Length; i++)
        {
            if (i == Level)
            {
                _upgradesMeshes[i].SetActive(true);
            }
            else
            {
                _upgradesMeshes[i].SetActive(false);
            }
        }

        _damageableBarricade.SetHealth(_healthValues[Level]);
        base.Upgrade();
    }
}