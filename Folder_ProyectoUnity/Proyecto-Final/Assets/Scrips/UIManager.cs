using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject ButtonOptionts;
    [SerializeField] private GameObject menuOptionts;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI moneyText;
    private PlayerController playerController;
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        if (playerController == null)
        {

        }
    }

    void Update()
    {
        if (playerController != null)
        {
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        healthText.text = "Health: " + playerController.Health.ToString();
        moneyText.text = "Money: " + playerController.Money.ToString();
    }
    public void ScenePlay()
    {
        SceneManager.LoadScene("Game1");
    }
    public void Pausa()
    {
        Time.timeScale = 0f;
        ButtonOptionts.SetActive(false);
        menuOptionts.SetActive(true);
    }
    public void Reanudar()
    {
        Time.timeScale = 1f;
        ButtonOptionts.SetActive(true);
        menuOptionts.SetActive(false);
    }
    public void Restar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        menuOptionts.SetActive(false);
        ButtonOptionts.SetActive(true);
        Time.timeScale = 1f;
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Menu()
    {
        DOTween.KillAll();
        SceneManager.LoadScene("Inicio");
    }
}
