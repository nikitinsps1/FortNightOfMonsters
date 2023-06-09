using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradersBarrierContainer : UpgradersContainer
{
    [SerializeField]
    private Upgrader[] _upgraders;

    private void Awake()
    {
        Upgraders = new Dictionary<int, Upgrader>();

        for (int i = 0; i < _upgraders.Length; i++)
        {
            Upgraders.Add(i, _upgraders[i]);
        }
    }

    public int GetIndex(Upgrader upgrader)
    {
        return Array.IndexOf(_upgraders, upgrader);
    }
}
