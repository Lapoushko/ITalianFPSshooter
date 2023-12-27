using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DoorOpener : MonoBehaviour
{
    [SerializeField] private bool isOpen;
    [SerializeField] GameObject door;
    public bool isCanOpen = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isCanOpen)
        {
            if (other.gameObject.tag.Contains("Player"))
            {
                isOpen = true;
                door.GetComponent<Animator>().SetBool("isOpen", isOpen);
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (isCanOpen)
        {
            if (other.gameObject.tag.Contains("Player"))
            {
                isOpen = false;
                door.GetComponent<Animator>().SetBool("isOpen", isOpen);
            }
        }
        
    }
}
