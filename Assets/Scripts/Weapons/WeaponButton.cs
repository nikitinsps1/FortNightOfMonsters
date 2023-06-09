using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]

public class WeaponButton : MonoBehaviour
{
    [SerializeField]
    private TypeWeapons _weapon;

    private Player _player;

    public Button ThisButton
    { get; private set; }

    [Inject]
    private void Construct(Player player)
    {
        _player = player;
    }

    private void Awake()
    {
        ThisButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        Action action = (delegate
        { _player.ThisArsenal.Change(_weapon); });

        ThisButton.onClick.AddListener(action.Invoke);
    }

    private void OnDisable()
    {
        ThisButton.onClick.RemoveAllListeners();
    }

}