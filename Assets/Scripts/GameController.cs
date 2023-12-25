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
    /// ����� �� ����� ����
    /// </summary>
    public void PauseOpen()
    {
        Time.timeScale = 0.0f;
        PauseGame.SetActive(true);
        CanvasGame.SetActive(false);
    }

    /// <summary>
    /// ������� ����� �� ����� ����
    /// </summary>
    public void PauseClose()
    {
        Time.timeScale = 1.0f;
        PauseGame.SetActive(false);
        CanvasGame.SetActive(true);
    }
    /// <summary>
    /// ����� � ����
    /// </summary>
    public void ReloadGame()
    {
        Time.timeScale = 0.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// ������ ������ � ������� ����
    /// </summary>
    public void Play()
    {
        Time.timeScale = 1.0f;
        MenuPanel.SetActive(false);
    } 
    /// <summary>
    /// ������ ������ ������
    /// </summary>
    public void DeathPlayer()
    {
        DeathPanel.SetActive(true);
        CanvasGame.SetActive(false);
        Time.timeScale = 0.0f;
    }

    /// <summary>
    /// ������ ������ ������
    /// </summary>
    public void WinPlayer()
    {
        WinPanel.SetActive(true);
        CanvasGame.SetActive(false);
        Time.timeScale = 0.0f;
    }
}
