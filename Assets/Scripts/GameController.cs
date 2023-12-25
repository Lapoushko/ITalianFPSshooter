using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private GameObject MenuPanel;
    [SerializeField] private GameObject PauseGame;
    [SerializeField] private GameObject DeathPanel;
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject CanvasGame;

    [SerializeField] private GameObject Player;

    private void Awake()
    {
        MenuPanel.SetActive(true);
        Time.timeScale = 0.0f;
    }
    /// <summary>
    /// Пауза во время игры
    /// </summary>
    public void PauseOpen()
    {
        Time.timeScale = 0.0f;
        PauseGame.SetActive(true);
        CanvasGame.SetActive(false);
    }

    /// <summary>
    /// Закрыть паузу во время игры
    /// </summary>
    public void PauseClose()
    {
        Time.timeScale = 1.0f;
        PauseGame.SetActive(false);
        CanvasGame.SetActive(true);
    }
    /// <summary>
    /// Выйти в меню
    /// </summary>
    public void ReloadGame()
    {
        Time.timeScale = 0.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Кнопка играть в главном меню
    /// </summary>
    public void Play()
    {
        Time.timeScale = 1.0f;
        MenuPanel.SetActive(false);
    } 
    /// <summary>
    /// Панель смерти игрока
    /// </summary>
    public void DeathPlayer()
    {
        DeathPanel.SetActive(true);
        CanvasGame.SetActive(false);
        Time.timeScale = 0.0f;
    }

    /// <summary>
    /// Панель победы игрока
    /// </summary>
    public void WinPlayer()
    {
        WinPanel.SetActive(true);
        CanvasGame.SetActive(false);
        Time.timeScale = 0.0f;
    }
}
