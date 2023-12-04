using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Sensivity Camera")]
    public float mouseSensivity = 0f;
    [SerializeField] LayerMask layerEnemy;
    [SerializeField] LayerMask layerShop;
    [SerializeField] float radiusTarget;
    [SerializeField] float speedAssist;

    [SerializeField] float mouseX, mouseY;

    [SerializeField] Transform playerBody;
    [SerializeField] float xRotation = 0f;

    [SerializeField] float distanceAim;

    InputManager inputManager;

    internal bool isAiming;
    Vector3 curEuler = Vector3.zero;

    ShopContainer shopContainer;
    ChangeWeaponsManager changeWeaponsManager;
    Camera cam;

    private void Awake()
    {
        Application.targetFrameRate = 120;
        inputManager = GameObject.Find("Input Manager").GetComponent<InputManager>();
        changeWeaponsManager = GameObject.Find("ShopManagerWeaponAndKit").GetComponent<ChangeWeaponsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        LookCalculate();
    }

    void LookCalculate() {
        mouseSensivity = inputManager.sensivity;
        float curMouseSensivity = mouseSensivity;

        Debug.DrawRay(transform.position, transform.forward, Color.black);
        Ray ray = new(transform.position, transform.forward);

        //Куда направлен взгляд игрока
        if (Physics.Raycast(ray, out RaycastHit target, 5, layerShop))
        {
            target.transform.GetChild(0).Rotate(0, (transform.rotation.y + 10) * Time.deltaTime * 10, 0);
            if (inputManager.isJoystick)
            {
                inputManager.ActiveBuyButton(true);
            }
            if (Input.GetKeyDown(KeyCode.G) || inputManager.buyButtonInfo.isDown)
            {
                int id = target.transform.GetComponent<ShopContainer>().Id;
                string type = target.transform.GetComponent<ShopContainer>().Type;
                int idContainer = target.transform.GetComponent<ShopContainer>().IdContainer;
                changeWeaponsManager.DistributionWeaponByIdAndType(id,type, idContainer);
            }
        }
        else inputManager.ActiveBuyButton(false);

        if (Physics.SphereCast(ray, radiusTarget, out RaycastHit enemy, 75, layerEnemy))
        {
            isAiming = true;
            curMouseSensivity = mouseSensivity / 2;
        }
        else { 
            isAiming = false;
            curMouseSensivity = mouseSensivity;
        }
        mouseX = inputManager.ValueLooking()[0] * curMouseSensivity * Time.deltaTime;
        mouseY = inputManager.ValueLooking()[1] * curMouseSensivity * Time.deltaTime;
        xRotation -= mouseY;
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
