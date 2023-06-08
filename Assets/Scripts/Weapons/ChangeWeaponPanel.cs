using System.Collections.Generic;
using UnityEngine;

public class ChangeWeaponPanel : MonoBehaviour
{
    [SerializeField]
    private ChangeWeaponButton[] _buttons;

    private Dictionary
        <int, ChangeWeaponButton> _dictionary;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _dictionary = new Dictionary<int, ChangeWeaponButton>();

        for (int i = 0; i < _buttons.Length; i++)
        {
            _dictionary
                .Add((int)_buttons[i].Type, _buttons[i]);

            _buttons[i].Init();
        }
    }

    public void OnBuyWeapon(TypeWeapons typeWeapons)
    {
        
        _buttons[(int)typeWeapons]
            .ThisButton.interactable = true;
    }
}