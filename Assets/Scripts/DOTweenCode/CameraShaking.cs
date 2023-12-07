using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraShaking : MonoBehaviour
{
    public void Shake()
    {
        gameObject.transform.DOShakePosition(0.15f,0.15f,10,90f,false,true,ShakeRandomnessMode.Harmonic).SetEase(Ease.InOutBounce);
    }
}
