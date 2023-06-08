using UnityEngine;
using YG;

[RequireComponent(typeof(InputPhone))]
[RequireComponent(typeof(InputPlayer))]

public class YandexChooseInput : MonoBehaviour
{
    [SerializeField]
    private Canvas _joysticks;

    private InputPhone _inputPhone;
    private InputPlayer _inputComputer;

    private string _deviceType;

    private void Awake()
    {
        _inputPhone = GetComponent<InputPhone>();
        _inputComputer = GetComponent<InputPlayer>();

        _deviceType = YandexGame.EnvironmentData.deviceType;
    }

    private void Start()
    {
        if (_deviceType == ContainerStrings.Desktop)
        {
            _inputComputer.enabled = true;
        }
        else
        {
            _joysticks.enabled = true;
            _inputPhone.enabled = true;
        }
    }
}
