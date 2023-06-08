using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIVideoIntro : MonoBehaviour
{
    [SerializeField]
    private Image _logo;

    [SerializeField]
    private Canvas _exitCanvas;

    [SerializeField]
    private Button
        _exitButton,
        _returnButton;

    private SceneChanger _sceneChanger;
    private Tweener _fadingLogo;

    public event Action OnLogoFaded;

    [Inject]
    private void Construct(SceneChanger sceneChanger)
    {
        _sceneChanger = sceneChanger;
    }

    private void Awake()
    {
        InitButtons();
    }

    private void Update()
    {
        if (Input.anyKeyDown && _exitCanvas.isActiveAndEnabled == false)
        {
            _exitCanvas.enabled = true;
            Time.timeScale = 0;
        }
    }

    private void InitButtons()
    {
        _exitButton.onClick.AddListener(
            delegate 
            {
                Time.timeScale = 1;
                _sceneChanger.Change(TypeScene.Fort); 
            });

        _returnButton.onClick.AddListener(
            delegate
            {
                Time.timeScale = 1;
                _exitCanvas.enabled = false;
            });
    }

    private void OnDisable()
    {
        _fadingLogo.Kill();
    }

    public void FadeLogo()
    {
        _fadingLogo =_logo.DOColor(Color.white, 5f)
             .SetEase(Ease.Linear)
             .OnComplete(() => OnLogoFaded.Invoke()); 
    }
}