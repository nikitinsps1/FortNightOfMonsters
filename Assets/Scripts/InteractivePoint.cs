using UnityEngine;
using Zenject;

public class InteractivePoint : MonoBehaviour
{
    [SerializeField] 
    private GameObject _enablingObject;

    [SerializeField] 
    private string _nameAction;

    private InteractiveButton _button;

    [Inject]
    private void Construct(InteractiveButton interactiveButton)
    {
        _button = interactiveButton;    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerHeroLogic>())
        {
            _button.gameObject.SetActive(true);

            _button.SetNewAction(_enablingObject, _nameAction);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerHeroLogic>())
        {
            _button.OffInteractive();
        }
    }
}