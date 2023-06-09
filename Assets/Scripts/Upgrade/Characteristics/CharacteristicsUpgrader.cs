using UnityEngine;
using Zenject;

public class CharacteristicsUpgrader : Upgrader
{
    [field: SerializeField]
    public TypeCharacteristics Type { get; private set; }

    [SerializeField]
    private int[] _values;

    private PlayerHeroLogic _player;


    [Inject]
    private void Construct(PlayerHeroLogic player)
    {
        _player = player;
    }

    private void OnValidate()
    {
        if (MaxLevel != _values.Length)
        {
            _values = new int[MaxLevel];
            Debug.Log("Количество улучшений приравнено к максимальному уровню");
        }
    }

    public int GetValue()
    {
        return _values[Level - 1];
    }

    public override void Upgrade()
    {
        if (Type == TypeCharacteristics.Health)
        {
            _player.ThisDamageable.SetHealth(_values[Level]);
        }
        base.Upgrade();
    }
}