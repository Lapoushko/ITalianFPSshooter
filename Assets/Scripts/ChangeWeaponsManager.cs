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
    /// ������� ������
    /// </summary>
    /// <param name="id">id ������</param>
    private void BuyWeapon(int salary)
    {   
        weapons[curWeaponId].SetActive(false);
        weapons[id].SetActive(true);
        curWeaponId = id;
        curWeaponType = type;
        MoneyManager.instance.RemoveMoney(salary);
        shopManager.OffObjectInContainer(idContainer);
        AudioManager.instance.Play("Buy");
        Debug.Log("������ �������!");
    }


    /// <summary>
    /// ������������� �� ������ ������ ��� ������ � ����  
    /// </summary>
    /// <param name="id">id ������</param>
    /// <param name="type">type ������</param>
    public void DistributionWeaponByIdAndType(int id,
        string type,
        int idContainer,
        int salary,
        bool isCanBuy)
    {
        this.idContainer = idContainer;
        this.id = id;
        this.type = type;
        if (MoneyManager.instance.isCanBuy(salary) && isCanBuy)
        {
            if (type.StartsWith("weapon"))
            {
                BuyWeapon(salary);
            }
            else
            {
                SetKit(salary);
            }
        }
        
    }
    /// <summary>
    /// ��������� �� ������ �������
    /// </summary>
    /// <param name="id">����� ������</param>
    /// <param name="type">��� ������</param>
    private void SetKit(int salary)
    {
        switch (type)
        {
            case "kit_AR":
                if (!curWeaponType.Contains("AR"))
                {
                    // TODO: ��������, ��� ������ �� �������� ����
                    AudioManager.instance.Play("Can'tBuy");
                    Debug.Log("��� ����� ������ �� ��������");
                }
                else if (shopManager.GetCanBuy(idContainer))
                {
                    ARKit[id].SetActive(true);
                    MoneyManager.instance.RemoveMoney(salary); //������������ ����
                    shopManager.OffObjectInContainer(idContainer);
                    Debug.Log("KIT: " + id + " " + type);
                    AudioManager.instance.Play("Buy");
                }
                break;
            case "kit_SHOTGUN":
                if (!curWeaponType.Contains("SHOTGUN"))
                {
                    AudioManager.instance.Play("Can'tBuy");
                    // TODO: ��������, ��� ������ �� �������� ����
                    Debug.Log("��� ����� ������ �� ��������");
                }
                else if (shopManager.GetCanBuy(idContainer))
                {
                    ShotgunKit[id].SetActive(true);
                    MoneyManager.instance.RemoveMoney(salary);
                    shopManager.OffObjectInContainer(idContainer);
                    Debug.Log("KIT: " + id + " " + type);
                    AudioManager.instance.Play("Buy");
                }
                break;
            case "kit_SMG":
                if (!curWeaponType.Contains("SMG"))
                {
                    // TODO: ��������, ��� ������ �� �������� ����
                    AudioManager.instance.Play("Can'tBuy");
                    Debug.Log("��� ����� ������ �� ��������");
                }
                else if (shopManager.GetCanBuy(idContainer))
                {
                    SMGKit[id].SetActive(true);
                    MoneyManager.instance.RemoveMoney(salary);
                    shopManager.OffObjectInContainer(idContainer);
                    Debug.Log("KIT: " + id + " " + type);
                    AudioManager.instance.Play("Buy");
                }            
                break;
        }
    }
}
