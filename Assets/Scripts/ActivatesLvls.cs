using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatesLvls : MonoBehaviour
{
    public GameObject activateLvl;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            activateLvl.SetActive(true);
        }
    }
}
