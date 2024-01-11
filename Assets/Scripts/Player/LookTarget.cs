using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LookTarget : MonoBehaviour
{
    [SerializeField] float mouseX, mouseY;
    [SerializeField] float xRotation = 0f;

    InputManager inputManager;
    public float mouseSensivity = 0f;
    [SerializeField] Transform playerBody;

    // Start is called before the first frame update
    private void Awake()
    {
        inputManager = GameObject.Find("Input Manager").GetComponent<InputManager>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float curMouseSensivity = mouseSensivity;
        mouseX = inputManager.ValueLooking()[0] * curMouseSensivity * Time.deltaTime;
        mouseY = inputManager.ValueLooking()[1] * curMouseSensivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.rotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
