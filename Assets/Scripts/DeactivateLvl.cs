using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateLvl : MonoBehaviour
{
    public GameObject deactivateLvl;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            deactivateLvl.SetActive(false);
        }
    }
}
