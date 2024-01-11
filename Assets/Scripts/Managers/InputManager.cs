using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Joysticks")]
    public FloatingJoystick JoystickMoving;
    public FloatingJoystick JoystickLooking;
    public bool isJoystick;
    public GameObject Joysticks;

    [Header("Sensevity")]
    public float sensivityJoystick;
    public float mouseSensivity;
    public float sensivity;

    [Header("Purchaches")]
    public UIButtonInfo buyButtonInfo;
    public Button buyButton;

    private void OnValidate()
    {
        SwitchInput(isJoystick);
    }

    public void SwitchInput(bool isJoystick)
    {
        Joysticks.SetActive(isJoystick);
        Cursor.lockState = (isJoystick) ? CursorLockMode.None : CursorLockMode.Locked;
        sensivity = SetMouseSensivity(isJoystick);
        buyButton.gameObject.SetActive(isJoystick);
    }

    float SetMouseSensivity(bool isJoystick)
    {
        float sensivity = (isJoystick) ? sensivityJoystick : mouseSensivity;
        return sensivity;
    }

    public float[] ValueMoving()
    {
        var arr = new float[2];

        float x = (isJoystick) 
            ? JoystickMoving.Horizontal 
            : Input.GetAxis("Horizontal");

        float z = (isJoystick) 
            ? JoystickMoving.Vertical 
            : Input.GetAxis("Vertical");
        arr[0] = x;
        arr[1] = z;
        return arr;
    }

    public float[] ValueLooking()
    {
        var arr = new float[2];

        float x = (isJoystick) 
            ? JoystickLooking.Horizontal 
            : Input.GetAxis("Mouse X");

        float z = (isJoystick) 
            ? JoystickLooking.Vertical 
            : Input.GetAxis("Mouse Y");

        arr[0] = x;
        arr[1] = z;
        return arr;
    }

    public void ActiveBuyButton(bool isActive)
    {
        buyButton.gameObject.SetActive(isActive);
    }
}
