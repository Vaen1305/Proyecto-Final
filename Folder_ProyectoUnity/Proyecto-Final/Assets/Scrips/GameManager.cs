using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int score = 0;
    [SerializeField] private int level = 1;
    private SimplyLinkedList<int> highScores = new SimplyLinkedList<int>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadHighScores();
        }
    }

    public void AddScore(int points)
    {
        score += points;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetLevel()
    {
        return level;
    }

    private void SaveScore()
    {
        if (IsHighScore(score))
        {
            InsertScore(score);
            SaveHighScores();
        }
    }

    private void LoadHighScores()
    {
        highScores = new SimplyLinkedList<int>();
        for (int i = 0; i < 10; ++i)
        {
            if (PlayerPrefs.HasKey("HighScore" + i))
            {
                highScores.InsertNodeAtEnd(PlayerPrefs.GetInt("HighScore" + i));
            }
        }
        SortHighScores();
    }

    private void SaveHighScores()
    {
        for (int i = 0; i < highScores.Count; ++i)
        {
            PlayerPrefs.SetInt("HighScore" + i, highScores.Get(i));
        }
        PlayerPrefs.Save();
    }

    public SimplyLinkedList<int> GetHighScores()
    {
        return highScores;
    }

    private void InsertScore(int newScore)
    {
        highScores.InsertNodeAtEnd(newScore);
        SortHighScores();
        if (highScores.Count > 10)
        {
            SimplyLinkedList<int> truncatedList = new SimplyLinkedList<int>();
            for (int i = 0; i < 10; i++)
            {
                truncatedList.InsertNodeAtEnd(highScores.Get(i));
            }
            highScores = truncatedList;
        }
    }

    private bool IsHighScore(int newScore)
    {
        if (highScores.Count < 10)
        {
            return true;
        }

        for (int i = 0; i < highScores.Count; i++)
        {
            if (newScore > highScores.Get(i))
            {
                return true;
            }
        }
        return false;
    }

    private void SortHighScores()
    {
        List<int> scores = new List<int>();
        for (int i = 0; i < highScores.Count; i++)
        {
            scores.Add(highScores.Get(i));
        }
        scores.Sort((a, b) => b.CompareTo(a));
        highScores = new SimplyLinkedList<int>();
        for (int i = 0; i < scores.Count; i++)
        {
            highScores.InsertNodeAtEnd(scores[i]);
        }
    }

    /*public void LoadNextScene(string sceneName)
    {
        SaveScore();
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }*/

    public void ResetScore()
    {
        score = 0;
    }

    public void GameOver()
    {
        SaveScore();
        SceneManager.LoadScene("GameOverScene");
    }
    public void BorrarTodo()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("Todos los PlayerPrefs han sido borrados.");
    }
}
