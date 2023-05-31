using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskPanel : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    [SerializeField]
    private TextMeshProUGUI _textMesh;

    public void OnReadyForInvasions()
    {
        _button.interactable = true;
        _textMesh.text = "Нажмите, чтобы начать вторжение";
    }

    public void OnSetNewTask(string task)
    {
        _button.interactable = false;
        _textMesh.text = task;
    }
}