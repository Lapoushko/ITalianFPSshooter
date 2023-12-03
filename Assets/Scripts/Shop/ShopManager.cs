using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ShopManager : MonoBehaviour
{
    public GameObject[] cubs;
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
    private void Start()
    {
        //Dict, ������� �������� ��� ������ �������
        Dictionary<string, List<GameObject>> allListMap =
            new Dictionary<string, List<GameObject>>
            {
                //�������� �� ������ ������
                { "shotgun", shotgunList },
                { "ar", arList },
                { "smg", smgList }
            };

        string name = "smg";
        List<GameObject> currencyList = allListMap[name];
       
        //� ����� ������ ����� � �����������
        for(int i = 0; i < cubs.Length; i++)
        {
            if (currencyList.Count == 0)
            {
                allListMap.Remove(name);
                currencyList = allListMap[getNameList(
                    allListMap, new Random().Next(0, allListMap.Count) )];
            }
            //���������� ��������� ����� ��� ������ ��� ��������(��������� ��� ���������� ������)
            Random random = new Random();
            int number = random.Next(0, currencyList.Count);
            GameObject kit = Instantiate(currencyList[number], cubs[i].transform);
            cubs[i].GetComponent<ShopContainer>().Id = currencyList[number].GetComponent<IdObject>().Id;
            currencyList.Remove(currencyList[number]);
        }
    }
    /// <summary>
    /// ������ ���������� ����� ��� ����� allListMap
    /// </summary>
    /// <param name="allListMap">Dict ���� �������</param>
    /// <param name="id">��������� id ������</param>
    /// <returns>��� ������</returns>
    private string getNameList(Dictionary<string, List<GameObject>> allListMap, int id)
    {
        List<string> strings = new List<string>(allListMap.Keys);
        return strings[id];
    }
}
