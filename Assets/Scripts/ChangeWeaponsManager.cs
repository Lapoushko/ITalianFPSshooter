using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeaponsManager : MonoBehaviour
{
    [SerializeField] GameObject[] weapons;
    [SerializeField] GameObject weaponArrayInspector;
    public int curWeaponId;
    public int nextWeaponId;
    public void ChangeWeapon(int id)
    {        
        weapons[curWeaponId].SetActive(false);
        weapons[id].SetActive(true);
        curWeaponId = id;
    }

    private void Start()
    {
        for(int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].activeInHierarchy)
            {
                curWeaponId = i;
                break;
            }
        }
    }
}
