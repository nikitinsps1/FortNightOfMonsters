using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;



public class TweenJoysticks : MonoBehaviour
{
    
    [SerializeField] 
    private Color 
        _normalColor,
        _pressColor;

    private Tween
        _scaling,
        _painting;

    private Image _image;
    private Transform _transform;


    private void Awake()
    {
        _image = GetComponent<Image>();
        _transform = GetComponent<Transform>();
    }

    public void OnPress()
    {
        KillAll();

        _transform.DOScale(1.1f, 4).SetEase(Ease.OutBack);
        _image.DOColor(_pressColor, 4);
    }


    private void OnUp()
    {
        KillAll();
        _transform.DOScale(1f, 3).SetEase(Ease.OutBack);
        _image.DOColor(_normalColor, 3);
    }


    private void OnDisable()
    {
        KillAll();
    }
    private void KillAll()
    {
        _scaling.Kill();
        _painting.Kill();

    }
}

