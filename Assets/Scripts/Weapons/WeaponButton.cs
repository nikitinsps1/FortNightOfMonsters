using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]

public class WeaponButton : MonoBehaviour
{
    [SerializeField]
    private TypeWeapons _weapon;

    private PlayerHeroLogic _player;

    public Button ThisButton
    { get; private set; }

    [Inject]
    private void Construct(PlayerHeroLogic player)
    {
        _player = player;
    }

    private void Awake()
    {
        ThisButton = GetComponent<Button>();
    }

    private void Start()
    {
        ThisButton
            .onClick
            .AddListener(() => _player.ThisArsenal.Change(_weapon));
    }

}