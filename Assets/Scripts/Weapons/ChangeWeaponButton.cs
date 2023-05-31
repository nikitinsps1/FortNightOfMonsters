using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class ChangeWeaponButton : MonoBehaviour
{
    [SerializeField] 
    private TypeWeapons _type;

    private Player _player;

    public Button ThisButton
    { get; private set; }

    public TypeWeapons Type => _type;

    [Inject]
    private void Construct(Player player)
    {
        _player = player;
    }

    public  void Init()
    {
        ThisButton = GetComponent<Button>();

        Action action = (delegate
        { _player.ThisArsenal.Change(_type); });

        ThisButton.onClick.AddListener(action.Invoke);
    }

}