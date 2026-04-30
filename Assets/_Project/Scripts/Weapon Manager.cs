using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private SO_WeaponData[] _availableWeapons;
    [SerializeField] private GameObject[] _weaponModels;

    private int _currentWeaponIndex = 0;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        EquipWeapon(0);
        _animator.SetBool("Gun", true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipWeapon(0);
            _animator.SetBool("Gun", true);
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipWeapon(1);
            _animator.SetBool("Gun", false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EquipWeapon(2);
            _animator.SetBool("Gun", false);
        }
    }

    void EquipWeapon(int index)
    {
        for (int i = 0; i < _weaponModels.Length; i++)
        {
            _weaponModels[i].SetActive(false);
        }

        _weaponModels[index].SetActive(true);
        _currentWeaponIndex = index;

        LaserSight currentLaser = _weaponModels[index].GetComponentInChildren<LaserSight>();

        if (currentLaser != null)
        {
            currentLaser.SetupLaser(_availableWeapons[index]);
        }
    }
}
