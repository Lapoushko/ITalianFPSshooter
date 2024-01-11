using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpeed : MonoBehaviour
{
    private PlayerMovement player;
    private ButtonUI buttonUI;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        buttonUI = GetComponent<ButtonUI>();
    }
    public void FastRun()
    {
        player.FastRun(buttonUI.timeEffect);
        buttonUI.StartChangeSpeed();
    }
}
