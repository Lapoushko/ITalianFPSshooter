using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    UIManagerPlayer UIManager;
    [SerializeField] public int money;
    GameController gameController;

    public override void Awake()
    {
        base.Awake();
        UIManager = FindFirstObjectByType<UIManagerPlayer>();
        gameController = FindFirstObjectByType<GameController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            TakeDamage(10);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            MoneyManager.instance.AddMoney(100);
        }
        else if (Input.GetKeyDown(KeyCode.M)){
            MoneyManager.instance.RemoveMoney(10);
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (Health <= 0)
        {
            gameController.DeathPlayer();
        }

        UIManager.durationTimer = 0f;

        UIManager.damageOverlay.color = new Color(
            UIManager.damageOverlay.color.r,
            UIManager.damageOverlay.color.g,
            UIManager.damageOverlay.color.b,
            0.125f);
    }
}
