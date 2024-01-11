using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Класс кнопки скорости
/// </summary>
public class ButtonUI : MonoBehaviour
{
    [Header("Button")]
    public GameObject buttonChanges;
    Button button;
    public bool IsCan;
    Image buttonImage;

    [Header("Times")]
    public float timeEffect;
    public float timeReload;
    protected void Awake()
    {
        button = buttonChanges.GetComponent<Button>();
        buttonImage = buttonChanges.GetComponent<Image>();
    }

    private void Update()
    {
        if (!button.interactable)
        {
            buttonImage.fillAmount += 1 / timeReload * Time.deltaTime;
        }
    }

    public void StartChangeSpeed()
    {
        IsCan = true;
        button.interactable = false;
        buttonImage.fillAmount = 0;
        StartCoroutine(CoroutineChangeSpeed(timeReload));
    }


    IEnumerator CoroutineChangeSpeed(float t)
    {
        yield return new WaitForSeconds(t);
        button.interactable = true;
    }
}
