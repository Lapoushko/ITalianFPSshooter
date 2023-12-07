using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public virtual void Awake()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
    }
}
