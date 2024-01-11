using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHealing : MonoBehaviour
{
    private PlayerHealing player;
    private ButtonUI buttonUI;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerHealing>();
        buttonUI = GetComponent<ButtonUI>();
    }
    public void Healing()
    {
        player.Healing(buttonUI.timeEffect);
        buttonUI.StartChangeSpeed();
    }
}
