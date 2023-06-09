﻿using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField]
    private ObjectPool _object;

    [SerializeField]
    private int _startAmount;

    private Transform _transform;

    private List<ObjectPool> _pool;
    public int StartAmount => _startAmount;

    public void FormPool(int count)
    {
        if (_pool == null)
        {
            _transform = GetComponent<Transform>();
            _pool = new List<ObjectPool>();

            for (int i = 0; i < count; i++)
            {
                CreateObject();
            }
        }
    }

    public virtual ObjectPool CreateObject()
    {
        ObjectPool newObject = Instantiate(_object, _transform);
        newObject.Init();

        _pool.Add(newObject);

        newObject.ThisGameObject.SetActive(false);
        return newObject;
    }

    public ObjectPool GetObject()
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            if (_pool[i].ThisGameObject.activeSelf == false)
            {
                return _pool[i];
            }
        }
        return CreateObject();
    }
}