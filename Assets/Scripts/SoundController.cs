using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.Play("Main");
    }
}
