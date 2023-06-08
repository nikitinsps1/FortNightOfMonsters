using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI _textMesh;

    [SerializeField] 
    private GameObject _panel;

    [SerializeField]
    private Transform _transform;

    private AudioContainer _audio;
    private Tween _tween;

    [Inject]
    private void Construct(AudioContainer audio)
    {
        _audio = audio;
    }

    public void ShowMessage(string newText)
    {
        _panel.SetActive(true);
        _audio.PlaySound(TypeSound.Radio, 0.1f);
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