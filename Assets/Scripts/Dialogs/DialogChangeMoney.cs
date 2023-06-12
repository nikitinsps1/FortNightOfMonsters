using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ChangeMoney", menuName = "Level/Dialog/Create new Change Money")]
public class DialogChangeMoney : DialogAction
{
    [SerializeField]
    private int _value;

    public override Action GetAction(DialogActionMediator mediator)
    {
        IsHaveNewTask = false;

        return delegate
        { mediator.SaveData.RefreshAmountMoney(_value); };
    }
}