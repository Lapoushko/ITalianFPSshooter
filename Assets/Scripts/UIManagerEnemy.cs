using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerEnemy : MonoBehaviour
{
    public Player player;
    public Enemy enemy;

    [Header("Health")]
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Image healthBarMax;
    [SerializeField] private Image healthBarCur;
    [SerializeField] private Gradient gradientHp;
    [SerializeField] private GameObject allObjects;

    Camera cam;
    void Awake()
    {
        player = FindAnyObjectByType<Player>().GetComponent<Player>();
        enemy = FindAnyObjectByType<Enemy>().GetComponent<Enemy>();

        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        healthBarCur.color = gradientHp.Evaluate(healthBarCur.fillAmount);
        healthText.color = healthBarCur.color;
        healthBarCur.fillAmount = (float)enemy.health / (float)enemy.maxHealth;
        healthText.text = enemy.health.ToString();

        //allObjects.transform.localRotation = Quaternion.LookRotation(transform.position - player.transform.position);
    }
}
