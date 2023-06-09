using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradersContainer : MonoBehaviour
{
    public Dictionary<int, Upgrader> Upgraders
    { get; protected set; }
}
