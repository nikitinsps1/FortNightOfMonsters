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
        _scaling1,
        _scaling2,
        _painting1,
        _painting2;

    [SerializeField]
    private Image
        _handle,
        _circle;

    [SerializeField]
    private Transform
        _handleTransform,
        _circleTransform;




    public void OnPress()
    {
        KillAll();
        _scaling1 = _handleTransform.DOScale(1.1f, 3).SetEase(Ease.OutBack);
        _scaling2 = _circleTransform.DOScale(1.1f, 3).SetEase(Ease.OutBack);

        _painting1 = _handle.DOColor(_pressColor, 3);
        _painting2 = _circle.DOColor(_pressColor, 3);
    }


    public void OnUp()
    {
        KillAll();

        _scaling1 = _handleTransform.DOScale(1f, 3).SetEase(Ease.OutBack);
        _scaling2 = _circleTransform.DOScale(1f, 3).SetEase(Ease.OutBack);

        _painting1 = _handle.DOColor(_normalColor, 4);
        _painting2 = _circle.DOColor(_normalColor, 4);
    }


    private void OnDisable()
    {
        KillAll();
    }
    private void KillAll()
    {
        _scaling1.Kill();
        _scaling2.Kill();
        _painting1.Kill();
        _painting2.Kill();

    }
}

