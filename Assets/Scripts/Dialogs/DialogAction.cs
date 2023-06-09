using System;
using UnityEngine;

public abstract class DialogAction : MonoBehaviour
{
    public abstract Action GetAction();
    public bool IsHaveNewTask
    { get; protected set; }
}