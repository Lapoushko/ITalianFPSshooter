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

    ChangeWeaponsManager changeWeaponsManager;
    LookTarget lookTarget;
    private Vector3 velocity;

    private void Awake()
    {
        Application.targetFrameRate = 120;
        inputManager = GameObject.Find("Input Manager").GetComponent<InputManager>();
        changeWeaponsManager = GameObject.Find("ShopManagerWeaponAndKit").GetComponent<ChangeWeaponsManager>();
        lookTarget = GameObject.FindAnyObjectByType<LookTarget>();
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
            if (target.transform.TryGetComponent(out ShopContainer container))
            { 
                if (inputManager.isJoystick)
                {
                    if (container.IsCanBuy)
                    {
                        inputManager.ActiveBuyButton(true);
                    }
                }
                if (Input.GetKeyDown(KeyCode.E) || inputManager.buyButtonInfo.isDown)
                {
                    int id = container.Id;
                    string type = container.Type;
                    int idContainer = container.IdContainer;
                    int salary = container.Price;
                    bool isCanBuy = container.IsCanBuy;
                    changeWeaponsManager.DistributionWeaponByIdAndType(id,
                        type,
                        idContainer,
                        salary,
                        isCanBuy);
                }
            }
        }
        else inputManager.ActiveBuyButton(false);

        if (Physics.SphereCast(ray, radiusTarget, out RaycastHit enemy, 75, layerEnemy))
        {
            isAiming = true;
            curMouseSensivity = mouseSensivity / 2;
            lookTarget.mouseSensivity = curMouseSensivity;
        }
        else { 
            isAiming = false;
            curMouseSensivity = mouseSensivity;
            lookTarget.mouseSensivity = curMouseSensivity;
        }

        transform.localRotation = Quaternion.Slerp(transform.localRotation, lookTarget.transform.rotation, 0.125f);
    }
}
