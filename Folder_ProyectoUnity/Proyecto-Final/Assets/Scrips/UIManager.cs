using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject ButtonOptionts;
    [SerializeField] private GameObject menuOptionts;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private List<TextMeshProUGUI> highScoreTexts;

    private PlayerController playerController;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        if (playerController == null)
        {
            Debug.LogWarning("PlayerController not found");
        }

        if (SceneManager.GetActiveScene().name == "GameOverScene")
        {
            DisplayHighScores();
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
        scoreText.text = "Score: " + GameManager.Instance.GetScore().ToString();
        levelText.text = "Level: " + GameManager.Instance.GetLevel().ToString();
    }

    public void UpdateLevelUI(int newLevel)
    {
        levelText.text = "Level: " + newLevel.ToString();
    }

    public void DisplayHighScores()
    {
        SimplyLinkedList<int> highScores = GameManager.Instance.GetHighScores();

        for (int i = 0; i < highScoreTexts.Count; ++i)
        {
            if (i < highScores.Count)
            {
                highScoreTexts[i].text = highScores.Get(i).ToString();
            }
            else
            {
                highScoreTexts[i].text = "0";
            }
        }
    }

    public void ScenePlay()
    {
        GameManager.Instance.ResetScore();
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

    public void BorrarTodo()
    {
        GameManager.Instance.BorrarTodo();
    }
}
