using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopContainer : MonoBehaviour
{
    /// <summary>
    /// Id ������ ��� ������
    /// </summary>
    public int Id;
    /// <summary>
    /// Type ������ (weapon or kit)
    /// </summary>
    public string Type;
    /// <summary>
    /// id ����������
    /// </summary>
    public int IdContainer;
    public bool IsCanBuy = true;
    /// <summary>
    /// ���� ������
    /// </summary>
    public int Price;
    public TMP_Text PriceText;
}
