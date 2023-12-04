using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeaponsManager : MonoBehaviour
{
    public string curWeaponType;

    [SerializeField] GameObject[] weapons;
    [SerializeField] GameObject weaponArrayInspector;

    [SerializeField] GameObject[] SMGKit;
    [SerializeField] GameObject[] ARKit;
    [SerializeField] GameObject[] ShotgunKit;
    public int curWeaponId;
    public int nextWeaponId;

    private ShopManager shopManager;
    private int idContainer;
    private int id;
    private string type;

    private void Awake()
    {
        shopManager = FindObjectOfType<ShopManager>();
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
    /// <summary>
    /// Покупка оружия
    /// </summary>
    /// <param name="id">id оружия</param>
    private void BuyWeapon()
    {   
        weapons[curWeaponId].SetActive(false);
        weapons[id].SetActive(true);
        curWeaponId = id;
        curWeaponType = type;
        shopManager.OffObjectInContainer(idContainer);
        Debug.Log("Оружие куплено!");
    }


    /// <summary>
    /// Распределение по номеру обвеса или оружия и типу  
    /// </summary>
    /// <param name="id">id оружия</param>
    /// <param name="type">type оружия</param>
    public void DistributionWeaponByIdAndType(int id, string type, int idContainer)
    {
        this.idContainer = idContainer;
        this.id = id;
        this.type = type;
        if (type.StartsWith("weapon"))
        {
            BuyWeapon();
        }
        else
        {
            SetKit();
        }
        
    }
    /// <summary>
    /// Установка на оружие обвесов
    /// </summary>
    /// <param name="id">номер обвеса</param>
    /// <param name="type">тип опвеса</param>
    private void SetKit()
    {
        switch (type)
        {
            case "kit_AR":
                if (!curWeaponType.Contains("AR"))
                {
                    // TODO: показать, что оружие не подходит явно
                    Debug.Log("Для этого оружия не подходит");
                }
                else if (shopManager.GetCanBuy(idContainer))
                {
                    ARKit[id].SetActive(true);
                    shopManager.OffObjectInContainer(idContainer);
                    Debug.Log("KIT: " + id + " " + type);
                }
                break;
            case "kit_SHOTGUN":
                if (!curWeaponType.Contains("SHOTGUN"))
                {
                    // TODO: показать, что оружие не подходит явно
                    Debug.Log("Для этого оружия не подходит");
                }
                else if (shopManager.GetCanBuy(idContainer))
                {
                    ShotgunKit[id].SetActive(true);
                    shopManager.OffObjectInContainer(idContainer);
                    Debug.Log("KIT: " + id + " " + type);
                }
                break;
            case "kit_SMG":
                if (!curWeaponType.Contains("SMG"))
                {
                    // TODO: показать, что оружие не подходит явно
                    Debug.Log("Для этого оружия не подходит");
                }
                else if (shopManager.GetCanBuy(idContainer))
                {
                    SMGKit[id].SetActive(true);
                    shopManager.OffObjectInContainer(idContainer);
                    Debug.Log("KIT: " + id + " " + type);
                }            
                break;
        }
    }
}
