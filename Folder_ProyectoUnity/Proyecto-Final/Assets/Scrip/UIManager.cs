using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject ButtonOptionts;
    [SerializeField] private GameObject menuOptionts;

    public void ScenePlay()
    {
        SceneManager.LoadScene("Game");
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
        SceneManager.LoadScene("Inicio");
    }
}
