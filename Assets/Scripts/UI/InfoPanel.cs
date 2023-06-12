using DG.Tweening;
using TMPro;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textMesh;

    [SerializeField]
    private GameObject _panel;

    [SerializeField]
    private Transform _transform;

    private Tween _tween;

    public void ShowMessage(string newText)
    {
        _panel.SetActive(true);
        _transform.localScale = Vector3.zero;

        _tween = _transform.DOScale(1, 1)
            .SetEase(Ease.OutBack);

        _textMesh.text = newText;
    }

    private void OnDisable()
    {
        _tween.Kill();
    }
}