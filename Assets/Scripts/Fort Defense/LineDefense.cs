using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LineDefense : MonoBehaviour
{
    [SerializeField]
    private Directions _directionInvasion;

    [SerializeField]
    private Guard[] _guards;

    [SerializeField]
    private Barricade _barricade;

    private List<Damageable> _frontiers;
    private Damageable _mainHouse;

    [Inject]
    private void Construct(MainHouse mainHouse)
    {
        _mainHouse = mainHouse.GetComponent<Damageable>();
    }

    private void Awake()
    {
        _frontiers = new List<Damageable>();
    }

    public void Alarm()
    {
        for (int i = 0; i < _guards.Length; i++)
        {
            if (_guards[i].isActiveAndEnabled)
            {
                StartCoroutine(_guards[i].StartShooting()); 
            }
        }
    }

    public List<Damageable> FormFrontiers()
    {
        _frontiers.Clear();

        if (_barricade.isActiveAndEnabled)
        {
            _frontiers.Add(_barricade.ThisDamageable);
        }

        for (int i = 0; i < _guards.Length; i++)
        {
            if (_guards[i].isActiveAndEnabled)

            {
                _frontiers.Add(_guards[i].ThisDamageable);
            }
        }

        _frontiers.Add(_mainHouse);

        return _frontiers;
    }
}