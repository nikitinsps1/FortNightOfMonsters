using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskPanel : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    [SerializeField]
    private TextMeshProUGUI _textMesh;

    private void Awake()
    {
        _button.onClick.AddListener(() => gameObject.SetActive(false));
    }

    public void SetNewTask(string text)
    {
        _button.interactable = false;
        _textMesh.text = text;
    }

    public void SetNewTask(string text, Action action)
    {
        SetNewTask(text);
        _button.interactable = true;
        _button.onClick.AddListener(action.Invoke);
    }
}