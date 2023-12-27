using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Генерация боссов
/// </summary>
public class EnemyGenerate : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies;
    public GameObject enemy;
    public bool DeadEnemy = false;

    public void Start()
    {
        int id = Random.Range(0, enemies.Count);
        enemy = enemies[id];
        enemy.SetActive(true);
    }
}
