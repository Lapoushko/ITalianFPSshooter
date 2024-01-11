using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private EnemyGenerate enemyGenerate;
    [SerializeField] private DoorOpener doorOpener;

    private void Update()
    {
        if (enemyGenerate != null)
        {
            if (enemyGenerate.DeadEnemy)
            {
                doorOpener.isCanOpen = true;
            }
        } 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
           ///
        }
    }
}
