using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    /// <summary>
    /// Текущее количество денег игрока
    /// </summary>
    public static MoneyManager instance;

    public int Money;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        Money = 10000;
    }

    public void AddMoney(int salary) => Money += salary;

    public void RemoveMoney(int salary) => Money -= salary;

    public bool isCanBuy(int salary) => Money >= salary;
}
