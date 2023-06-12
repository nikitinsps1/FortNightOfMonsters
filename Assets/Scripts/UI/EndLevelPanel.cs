using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;
using Zenject;

public class EndLevelPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textMeshPro;

    [SerializeField]
    private Button _button;

    private Transform _transform;
    private Tween _scaleImage;
    private SaveData _saveData;
    private Action _action;

    [Inject]
    private void Construct(SaveData saveData)
    {
        _saveData = saveData;
    }

    private void Awake()
    {
        _transform = transform;
    }

    public void Init(Action action, string text)
    {
        _action = action;

        if (_saveData.NumberLevel % 3 == 0 && _saveData.NumberLevel != 0)
        {
            _button.onClick.AddListener(YandexGame.FullscreenShow);
            YandexGame.CloseFullAdEvent += _action;
        }
        else
        {
            _button.onClick.AddListener(_action.Invoke);
        }


        _transform.localScale = Vector3.zero;

        _scaleImage = _transform.DOScale(1, 1)
            .SetEase(Ease.OutBack)
            .OnComplete(() => Time.timeScale = 0);

        _textMeshPro.text = text;
    }

    private void OnDisable()
    {
        if (_action != null)
        {
            YandexGame.CloseFullAdEvent -= _action;
        }
        Time.timeScale = 1;
        _scaleImage.Kill();
        _scaleImage = null;
    }
}