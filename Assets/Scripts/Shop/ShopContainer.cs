using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopContainer : MonoBehaviour
{
    /// <summary>
    /// Id обвеса или оружия
    /// </summary>
    public int Id;

    /// <summary>
    /// Type обвеса (weapon or kit)
    /// </summary>
    public string Type;
    /// <summary>
    /// id контейнера
    /// </summary>
    public int IdContainer;
    public bool IsCanBuy = true;
}
