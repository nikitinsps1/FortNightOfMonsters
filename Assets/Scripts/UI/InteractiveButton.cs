using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveButton : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    [SerializeField] 
    private TextMeshProUGUI _nameInteractive;

    private Transform _transform;
    private Tween _scaling;

    private void Awake()
    {
        _transform = transform;
    }

    private void OnEnable()
    {
        _transform.localScale = Vector3.zero;

        _scaling = _transform.DOScale(1, 1)
            .SetEase(Ease.OutBack);

        _button.onClick.AddListener
            (delegate { gameObject.SetActive(false); });
    }

    private void OnDisable()
    {
        OffInteractive();
    }

    public void SetNewAction(GameObject enabledObject, string name)
    {
        _button.onClick.AddListener
            (delegate { enabledObject.SetActive(true); });

        _nameInteractive.text = name;
    }

    public void OffInteractive()
    {
        _scaling.Kill();
        _nameInteractive.text = "";
        _button.onClick.RemoveAllListeners();
        gameObject.SetActive(false);
    }
}
