using UnityEngine;
using YG;
using Zenject;

[RequireComponent(typeof(InputPhone))]
[RequireComponent(typeof(InputPlayer))]

public class YandexChooseInput : MonoBehaviour
{
    [SerializeField]
    private Canvas _joystics;

    private InputPhone _inputPhone;
    private InputPlayer _inputComputer;

    private Player _player;
    private string _deviceType;

    [Inject]
    private void Construct(Player player)
    {
        _player = player;
    }



    private void Awake()
    {
        _inputPhone = GetComponent<InputPhone>();
        _inputComputer = GetComponent<InputPlayer>();
        _deviceType = YandexGame.EnvironmentData.deviceType;
    }

    private void Start()
    {
        if (_deviceType == "desktop")
        {
            _inputComputer.enabled = true;
        }
        else
        {
            _joystics.enabled = true;
            _inputPhone.enabled = true;
        }
    }
}
