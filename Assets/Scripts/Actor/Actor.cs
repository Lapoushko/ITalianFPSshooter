using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    private int health;
    public int maxHealth;

    [SerializeField]
    public int Health
    {
        get { return health; }
        set
        {
            health = Mathf.Min(value, maxHealth);
        }
    }
    public virtual void Awake()
    {
        Health = maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
    }
}
