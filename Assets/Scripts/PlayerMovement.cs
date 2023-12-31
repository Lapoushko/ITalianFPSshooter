using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    CharacterController controller;

    [Header("Camera")]
    Camera cam;
    [SerializeField] float maxFov;
    [SerializeField] float minFov;
    [SerializeField] float speedzooom;
    [SerializeField] float avgFov;

    [Header("Movement Player")]
    public float minSpeedX;
    public float minSpeedY;
    [SerializeField] float maxSpeedX;
    public float curSpeed;

    [Header("Fast Run")]
    public bool isChangedSpeed;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    readonly float gravity = -9.81f;
    Vector3 velocity;

    bool isGrounded;
    bool isMoving;

    InputManager inputManager;
    ChangeWeaponsManager changeWeaponsManager;

    [Header("Effects")]
    [SerializeField] GameObject speedParticles;

    [SerializeField] int idWeapon;

    private GameController gameController;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        inputManager = GameObject.Find("Input Manager").GetComponent<InputManager>();
        changeWeaponsManager = GameObject.Find("ShopManagerWeaponAndKit").GetComponent<ChangeWeaponsManager>();
        cam = Camera.main;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void Start()
    {
        cam.fieldOfView = minFov;
        curSpeed = minSpeedX;
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
        CheckChangeSpeed();
    }

    private void LateUpdate()
    {
        if (isChangedSpeed)
        {
            MovingFOV(maxFov);
        }
        else
        {
            if (isMoving) MovingFOV(avgFov); else MovingFOV(minFov);
        }     
       // speedParticles.transform.position = transform.position;
    }

    void CheckChangeSpeed()
    {
        if (inputManager.IsChangeSpeed())
        {
            FastRun();
            inputManager.isCanSpeedMoving = false;
        }
    }

    void Moving()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }
        float x = inputManager.ValueMoving()[0];
        float z = inputManager.ValueMoving()[1];

        isMoving = (x != 0f || z != 0f) ? true : false;

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(curSpeed * Time.deltaTime * move);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(minSpeedY * -2 * gravity);
        }
    }

    void FastRun()
    {
        curSpeed = maxSpeedX;
        isChangedSpeed = true;
        speedParticles.SetActive(true);
        StartCoroutine(ReturnSpeedCoroutine(inputManager.timeForSpeedRunning));   
    }

    void MovingFOV(float target)
    {
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, target, Time.deltaTime * speedzooom);
    }

    IEnumerator ReturnSpeedCoroutine(float t)
    {
        yield return new WaitForSeconds(t);
        curSpeed = minSpeedX;
        isChangedSpeed = false;
        speedParticles.SetActive(false);
    }


}
