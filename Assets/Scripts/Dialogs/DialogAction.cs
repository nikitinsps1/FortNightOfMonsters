using System;
using UnityEngine;

public abstract class DialogAction: ScriptableObject
{
    public abstract Action GetAction(DialogActionMediator mediator);
    public bool IsHaveNewTask
    { get; protected set; }
}