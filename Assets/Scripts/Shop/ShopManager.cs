using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ShopManager : MonoBehaviour
{
    public GameObject[] cubs;
    /// <summary>
    /// Список обвесов для shotgun
    /// </summary>
    [Header("Shotgun")]
    [SerializeField] private List<GameObject> shotgunList;

    /// <summary>
    /// Список обвесов для AR
    /// </summary>
    [Header("AR")]
    [SerializeField] private List<GameObject> arList;

    /// <summary>
    /// Список обвесов для SMG
    /// </summary>
    [Header("SMG")]
    [SerializeField] private List<GameObject> smgList;
    private void Start()
    {
        //Dict, который содержит все списки обвесов
        Dictionary<string, List<GameObject>> allListMap =
            new Dictionary<string, List<GameObject>>
            {
                //Добавляю по именам списки
                { "shotgun", shotgunList },
                { "ar", arList },
                { "smg", smgList }
            };

        string name = "smg";
        List<GameObject> currencyList = allListMap[name];
       
        //В кубах создаёт обвес с приоритетом
        for(int i = 0; i < cubs.Length; i++)
        {
            if (currencyList.Count == 0)
            {
                allListMap.Remove(name);
                currencyList = allListMap[getNameList(
                    allListMap, new Random().Next(0, allListMap.Count) )];
            }
            //Появляется случайный обвес или оружие для магазина(Приоритет для выбранного оружия)
            Random random = new Random();
            int number = random.Next(0, currencyList.Count);
            GameObject kit = Instantiate(currencyList[number], cubs[i].transform);
            cubs[i].GetComponent<ShopContainer>().Id = currencyList[number].GetComponent<IdObject>().Id;
            currencyList.Remove(currencyList[number]);
        }
    }
    /// <summary>
    /// Геттер случайного имени для ключа allListMap
    /// </summary>
    /// <param name="allListMap">Dict всех списков</param>
    /// <param name="id">Случайный id списка</param>
    /// <returns>Имя списка</returns>
    private string getNameList(Dictionary<string, List<GameObject>> allListMap, int id)
    {
        List<string> strings = new List<string>(allListMap.Keys);
        return strings[id];
    }
}
