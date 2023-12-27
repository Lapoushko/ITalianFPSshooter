using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

/// <summary>
/// Заполнение магазина товарами со свойствами
/// </summary>
public class ShopManager : MonoBehaviour
{
    public GameObject shop;
    /// <summary>
    /// Имя для currensyList
    /// </summary>
    [SerializeField] private string nameList;
    /// <summary>
    /// Все слоты для предметов покупки
    /// </summary>
    public GameObject[] containers;
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

    /// <summary>
    /// Случайное заполнение контейнеров с приоритетом выбранного оружия
    /// </summary>
    private void Start()
    {
        //Dict, который содержит все списки обвесов
        Dictionary<string, List<GameObject>> allListMap =
            new Dictionary<string, List<GameObject>>
            {
                //Добавляю по именам(ключ) списки
                { "shotgun", shotgunList },
                { "ar", arList },
                { "smg", smgList }
            };
        //приоритетное оружие
        List<GameObject> currencyList = allListMap[nameList];
       
        //В кубах создаёт обвес (с приоритетом выбранного оружия)
        for(int i = 0; i < containers.Length; i++)
        {
            if (currencyList.Count == 0)
            {
                allListMap.Remove(nameList);
                currencyList = allListMap[GetNameList(
                    allListMap, new Random().Next(0, allListMap.Count) )];
            }
            //Появляется случайный обвес или оружие для магазина(Приоритет для выбранного оружия)
            Random random = new Random();
            int number = random.Next(0, currencyList.Count);
            Instantiate(currencyList[number], containers[i].transform);

            ShopContainer curShopContainer = containers[i].GetComponent<ShopContainer>();
            IdObject idObject = currencyList[number].GetComponent<IdObject>();

            curShopContainer.Id = idObject.Id;
            curShopContainer.Type = idObject.Type;
            curShopContainer.IdContainer = i;
            curShopContainer.Price = idObject.Price;
            curShopContainer.PriceText.text = curShopContainer.Price.ToString() + "$";
            currencyList.Remove(currencyList[number]);
        }
    }

    /// <summary>
    /// Геттер случайного имени для ключа allListMap
    /// </summary>
    /// <param name="allListMap">Dict всех списков</param>
    /// <param name="id">Случайный id списка</param>
    /// <returns>Имя списка</returns>
    private string GetNameList(Dictionary<string, List<GameObject>> allListMap, int id)
    {
        List<string> strings = new List<string>(allListMap.Keys);
        return strings[id];
    }
    /// <summary>
    /// Отключить дочерний объект после покупки
    /// </summary>
    /// <param name="id">id контейнера, в котором содержится </param>

    public void OffObjectInContainer(int id)
    {
        shop = containers[id];
        containers[id].transform.GetChild(0).gameObject.SetActive(false);
        containers[id].GetComponent<ShopContainer>().IsCanBuy = false;
        containers[id].GetComponent<ShopContainer>().PriceText.text = "Куплено!";
        containers[id].GetComponent<ShopContainer>().PriceText.color = Color.red;
    }

    public bool GetCanBuy(int id)
    {
        return containers[id].GetComponent<ShopContainer>().IsCanBuy;
    }
}
