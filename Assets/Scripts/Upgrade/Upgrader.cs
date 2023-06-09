using UnityEngine;

public abstract class Upgrader : MonoBehaviour
{
    [SerializeField]
    private int[] _costs;

    public int Level
    { get; private set; }

    public int MaxLevel => _costs.Length;

    public int GetCost()
    {
        return _costs[Level];
    }

    public bool AllDone()
    {
        if (Level == MaxLevel)
        {
            return true;
        }
        return false;
    }

    public virtual void Upgrade()
    {
        Level++;
    }
}