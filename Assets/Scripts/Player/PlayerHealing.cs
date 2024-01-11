using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// Класс восстановления hp
/// </summary>
public class PlayerHealing : MonoBehaviour
{
    private Player player;
    private float timeEffect;

    private void Awake()
    {
        player = GetComponent<Player>(); 
    }

    public void Healing(float time)
    {
        this.timeEffect = time;
        StartCoroutine(CoroutineHealing());
    }

    IEnumerator CoroutineHealing()
    {
        float regenTime = 0;
        while (regenTime < timeEffect)
        {
            player.Health += 2;
            regenTime += 0.1f;
            yield return null;
        }
        yield break; // stop the coroutine after 5 seconds
    }
}
