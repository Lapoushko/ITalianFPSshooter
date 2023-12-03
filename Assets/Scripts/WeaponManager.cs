using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class WeaponManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform attackPoint;
    [SerializeField] GameObject bullet;
    [SerializeField] ParticleSystem muzzleFlash;
    public float shootForce, upwardForce;
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;

    public int maxAmmo;
    public int ammoPerTap;
    public bool allowButtonHold;

    public int ammo, ammoShot;

    bool shooting, reloading, isCanShoot;
    public bool allowInvoke = true;

    [Header("UI")]
    [SerializeField] private TMP_Text textAmmo;
    [SerializeField] private Image reloadImage;
    Camera cam;
    public LayerMask layerMask;

    [Header("Managers")]
    Look look;
    InputManager inputManager;
    CameraShaking camShaking;
    ContainerKit containerKit;

    [Space]
    [Header("Camera Shaker")]
    [SerializeField] float magnitude;
    [SerializeField] float roughness;
    [SerializeField] float fadeInTime;
    [SerializeField] float fadeOutTime;
    private void Awake()
    {
        inputManager = GameObject.Find("Input Manager").GetComponent<InputManager>();;
        ammo = maxAmmo;
        isCanShoot = true;

        cam = Camera.main;
        textAmmo.text = ammo.ToString();
        look = cam.GetComponent<Look>();
        camShaking = cam.GetComponent<CameraShaking>();
        containerKit = gameObject.GetComponent<ContainerKit>();
    }

    private void Start()
    {
        for (int i = 0; i < containerKit.kits.Length; i++)
        {
            containerKit.kits[i].active = false;
        }
    }
    void Update()
    {
        MyInput();
        //Debug.DrawRay(attackPoint.transform.position, transform.forward, Color.red);
    }

    void MyInput()
    {
        if (ammo <= 0)
        {
            reloadImage.fillAmount += 1 / reloadTime * Time.deltaTime;
        }

        if (look.isAiming && allowButtonHold && inputManager.isJoystick) shooting = true;
        else if (!look.isAiming && allowButtonHold && inputManager.isJoystick) shooting = false;
        if (!inputManager.isJoystick)
        {
            if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
            else shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }


        if (isCanShoot && shooting && !reloading && ammo > 0)
        {
            ammoShot = 0;
            Shoot();
        }

        if ((isCanShoot && shooting && !reloading && ammo <= 0) ||
            (!shooting && !reloading && ammo <= 0))
        {
            Reloading();
        }
    }

    // Update is called once per frame

    void Shoot()
    {
        ammo--;
        ammoShot++;
        textAmmo.text = ammo.ToString();
        camShaking.Shake();
        isCanShoot = false;
        //StartCoroutine(cameraShake.Shake(.05f, .05f));
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Vector3 targetPoint;

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75);
        }

        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);

        muzzleFlash.Play();

        currentBullet.transform.forward = directionWithSpread.normalized;

        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(cam.transform.up * upwardForce, ForceMode.Impulse);

        //transform.DOMoveZ(transform.position.z - 0.2f, timeBetweenShooting).SetEase(Ease.InOutSine).SetLoops(2, LoopType.Yoyo);
        gameObject.transform.DOShakePosition(timeBetweenShooting, 0.05f, 10, 90f, false, true, ShakeRandomnessMode.Harmonic).SetEase(Ease.InOutBounce);

        if (allowInvoke)
        {
            Invoke(nameof(ResetShot), timeBetweenShooting);
            allowInvoke = false;
        }

        if (ammoShot < ammoPerTap && ammo > 0)
        {
            Invoke(nameof(Shoot), timeBetweenShots);
        }
    }

    void ResetShot()
    {
        isCanShoot = true;
        allowInvoke = true;
    }

    private void Reloading()
    {
        shooting = false;
        reloading = true;
        textAmmo.enabled = false;

        Invoke(nameof(ReloadFinishing), reloadTime);
    }

    void ReloadFinishing()
    {
        ammo = maxAmmo;
        ammoShot = 0;
        textAmmo.text = ammo.ToString();
        textAmmo.color = Color.white;
        reloading = false;
        reloadImage.fillAmount = 0;
        textAmmo.enabled = true;
    }
}
