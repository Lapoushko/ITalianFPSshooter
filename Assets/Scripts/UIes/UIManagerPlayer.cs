using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerPlayer : MonoBehaviour
{
    public Player player;

    [Header("Health")]
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Image healthBarMax;
    [SerializeField] private Image healthBarCur;
    [SerializeField] private Gradient gradientHp;

    [Header("DamageOverlay")]
    public Image damageOverlay;
    [SerializeField] private float duration;
    [SerializeField] private float fadeSpeed;

    [Header("Money")]
    [SerializeField] private TMP_Text moneyText;

    public float durationTimer;
    void Awake()
    {
        player = FindAnyObjectByType<Player>().GetComponent<Player>();
        damageOverlay.color = new Color(
            damageOverlay.color.r,
            damageOverlay.color.g,
            damageOverlay.color.b,
            0);
    }

    // Update is called once per frame
    void Update()
    {
        healthBarCur.color = gradientHp.Evaluate(healthBarCur.fillAmount);
        healthText.color = healthBarCur.color;
        healthBarCur.fillAmount = (float)player.Health / (float)player.maxHealth;
        healthText.text = player.Health.ToString();
        moneyText.text = MoneyManager.instance.Money.ToString();
        UpdateEffects();
    }

    private void UpdateEffects()
    {
        if (damageOverlay.color.a > 0)
        {
            durationTimer += Time.deltaTime;
            if (durationTimer > duration)
            {
                float tempAlpha = damageOverlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                damageOverlay.color = new Color(damageOverlay.color.r, damageOverlay.color.g, damageOverlay.color.b, tempAlpha);
            }
        }
    }
}
