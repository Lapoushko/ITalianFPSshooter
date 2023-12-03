using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeaponsManager : MonoBehaviour
{
    [SerializeField] GameObject[] weapons;
    [SerializeField] GameObject weaponArrayInspector;
    public int curWeaponId;
    public int nextWeaponId;
    private void BuyWeapon(int id)
    {   
        weapons[curWeaponId].SetActive(false);
        weapons[id].SetActive(true);
        curWeaponId = id;
    }

    private void Start()
    {
        weapons[0].SetActive(true);
        for(int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].activeInHierarchy)
            {
                curWeaponId = i;
                break;
            }
        }
    }

    public void ValidateId(int id)
    {
        if (id < 10)
        {
            BuyWeapon(id);
        }
        else if(id > 9 && id < 100)
        {
            if (id > 9 && id < 20)
            {

            }
        }
    }

    private void SetKit(int id)
    {
        Debug.Log("KIT");
    }
}
