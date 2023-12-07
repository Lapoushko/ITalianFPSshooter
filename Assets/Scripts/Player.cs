using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    UIManagerPlayer UIManager;

    public override void Awake()
    {
        base.Awake();
        UIManager = FindFirstObjectByType<UIManagerPlayer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            TakeDamage(10);
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        UIManager.durationTimer = 0f;

        UIManager.damageOverlay.color = new Color(
            UIManager.damageOverlay.color.r,
            UIManager.damageOverlay.color.g,
            UIManager.damageOverlay.color.b,
            0.125f);
    }
}
