using System;
using UnityEngine;
using Zenject;

public class DialogChangeMoney : DialogAction
{
    [SerializeField]
    private int _value;

    private SaveData _saveData;

    [Inject]
    private void Construct(SaveData saveData)
    {
        _saveData = saveData;
    }

    public override Action GetAction()
    {
        IsHaveNewTask = false;

        return delegate
        { _saveData.RefreshAmountMoney(_value); };
    }
}