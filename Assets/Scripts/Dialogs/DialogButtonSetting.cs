using System;
using UnityEngine;

[Serializable]
public struct DialogButtonSetting
{
    [field: SerializeField, TextArea(1, 20)]
    public string TextAfter
    { get; private set; }


    [field: SerializeField]
    public DialogAction DialogAction
    { get; private set; }

    public bool HaveNewTask
    {
        get
        {
            if (DialogAction != null)
            {
                if (DialogAction.IsHaveNewTask)
                {
                    return true;
                }
            }
            return false;
        }
    }
}