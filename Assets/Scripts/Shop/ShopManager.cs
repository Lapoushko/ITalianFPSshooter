using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class ShopManager : MonoBehaviour
{
    /// <summary>
    /// ��� ��� currensyList
    /// </summary>
    [SerializeField] private string nameList;
    /// <summary>
    /// ��� ����� ��� ��������� �������
    /// </summary>
    public GameObject[] containers;
    /// <summary>
    /// ������ ������� ��� shotgun
    /// </summary>
    [Header("Shotgun")]
    [SerializeField] private List<GameObject> shotgunList;

    /// <summary>
    /// ������ ������� ��� AR
    /// </summary>
    [Header("AR")]
    [SerializeField] private List<GameObject> arList;

    /// <summary>
    /// ������ ������� ��� SMG
    /// </summary>
    [Header("SMG")]
    [SerializeField] private List<GameObject> smgList;

    /// <summary>
    /// ��������� ���������� ����������� � ����������� ���������� ������
    /// </summary>
    private void Start()
    {
        //Dict, ������� �������� ��� ������ �������
        Dictionary<string, List<GameObject>> allListMap =
            new Dictionary<string, List<GameObject>>
            {
                //�������� �� ������(����) ������
                { "shotgun", shotgunList },
                { "ar", arList },
                { "smg", smgList }
            };
        //������������ ������
        List<GameObject> currencyList = allListMap[nameList];
       
        //� ����� ������ ����� (� ����������� ���������� ������)
        for(int i = 0; i < containers.Length; i++)
        {
            if (currencyList.Count == 0)
            {
                allListMap.Remove(nameList);
                currencyList = allListMap[GetNameList(
                    allListMap, new Random().Next(0, allListMap.Count) )];
            }
            //���������� ��������� ����� ��� ������ ��� ��������(��������� ��� ���������� ������)
            Random random = new Random();
            int number = random.Next(0, currencyList.Count);
            Instantiate(currencyList[number], containers[i].transform);

            containers[i].GetComponent<ShopContainer>().Id = currencyList[number].GetComponent<IdObject>().Id;
            containers[i].GetComponent<ShopContainer>().Type = currencyList[number].GetComponent<IdObject>().Type;
            containers[i].GetComponent<ShopContainer>().IdContainer = i;
            currencyList.Remove(currencyList[number]);
        }
    }

    /// <summary>
    /// ������ ���������� ����� ��� ����� allListMap
    /// </summary>
    /// <param name="allListMap">Dict ���� �������</param>
    /// <param name="id">��������� id ������</param>
    /// <returns>��� ������</returns>
    private string GetNameList(Dictionary<string, List<GameObject>> allListMap, int id)
    {
        List<string> strings = new List<string>(allListMap.Keys);
        return strings[id];
    }
    /// <summary>
    /// ��������� �������� ������ ����� �������
    /// </summary>
    /// <param name="id">id ����������, � ������� ���������� </param>

    public void OffObjectInContainer(int id)
    {
        containers[id].transform.GetChild(0).gameObject.SetActive(false);
        containers[id].GetComponent<ShopContainer>().IsCanBuy = false;
    }

    public bool GetCanBuy(int id)
    {
        return containers[id].GetComponent<ShopContainer>().IsCanBuy;
    }
}
