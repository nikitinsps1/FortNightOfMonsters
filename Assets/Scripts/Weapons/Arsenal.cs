using System;
using System.Collections.Generic;
using UnityEngine;

public class Arsenal : MonoBehaviour
{
    [SerializeField] 
    private Weapon[] _weapons;

    private Dictionary<int, Weapon> _weaponsDictionary;

    public Weapon CurrentWeapon { get; private set; }

    public event Action<TypeWeapons> OnChangedWeapon;

    public void Init()
    {
 
        _weaponsDictionary = new Dictionary<int, Weapon>();

        for (int i = 0; i < _weapons.Length; i++)
        {
            _weaponsDictionary
                .Add((int)_weapons[i].Type, _weapons[i]);
        }


        CurrentWeapon = _weapons[0];
        Change(CurrentWeapon.Type);
    }

    public void Attack()
    {
        CurrentWeapon.Attack();
    }

    public void StopAttack()
    {
        CurrentWeapon.StopAttack();
    }

    public void Change(TypeWeapons weapon)
    {
        CurrentWeapon.gameObject.SetActive(false);

        _weaponsDictionary
            [(int)weapon].gameObject.SetActive(true);

        CurrentWeapon = _weaponsDictionary[(int)weapon];

        OnChangedWeapon?.Invoke(weapon);
    }
}