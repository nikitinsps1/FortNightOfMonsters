using System.Collections.Generic;
using UnityEngine;

public abstract class PoolContainer<Spawnable>: MonoBehaviour
{
    public Dictionary <int, Pool> 
        Pools { get;set; }

    private void Awake()
    {
        InitDictionary();
    }

    private Pool ChoosePool(int indexPool)
    {
        return Pools[indexPool];
    }

    protected virtual void InitDictionary()
    {
        foreach (var item in Pools)
        {
            if (item.Value.StartAmount > 0)
            {
                item.Value.FormPool(item.Value.StartAmount);
            }
        }
    }

    public ObjectPool GetObject
        (int indexPool, Vector3 position, Quaternion rotation)
    {
        ObjectPool  spawnObject 
            = ChoosePool(indexPool).GetObject();

        spawnObject.ThisGameObject.SetActive(true);

        Vector3 newPosition = new Vector3 (
            position.x,
            spawnObject.ThisTransform.position.y, 
            position.z);

        spawnObject
            .ThisTransform
            .SetLocalPositionAndRotation
            (newPosition, rotation);

        spawnObject.ThisTransform.position = newPosition;
        return spawnObject;
    }
}
