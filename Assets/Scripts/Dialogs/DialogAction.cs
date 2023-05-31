using System;
using UnityEngine;

public abstract class DialogAction:  MonoBehaviour 
{
    public abstract Action GetEvent();

    public bool HaveNewTask 
    { get; protected set; }
}