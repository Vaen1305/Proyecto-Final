using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int score = 0;
    [SerializeField] private int level = 1;
    [SerializeField] private HighScoreData highScoreData;

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

    public void IncrementLevel()
    {
        level++;
        UIManager.Instance.UpdateLevelUI(level);
    }

    public void ResetScore()
    {
        score = 0;
        level = 1;
    }

    public void GameOver()
    {
        SaveScore();
        SceneManager.LoadScene("GameOverScene");
    }

    public SimplyLinkedList<int> GetHighScores()
    {
        return highScoreData.highScores;
    }

    private void SaveScore()
    {
        if (IsHighScore(score))
        {
            InsertScore(score);
            SortHighScores();
        }
    }

    private void InsertScore(int newScore)
    {
        highScoreData.highScores.InsertNodeAtEnd(newScore);
        SortHighScores();
        if (highScoreData.highScores.Count > 10)
        {
            SimplyLinkedList<int> truncatedList = new SimplyLinkedList<int>();
            for (int i = 0; i < 10; ++i)
            {
                truncatedList.InsertNodeAtEnd(highScoreData.highScores.Get(i));
            }
            highScoreData.highScores = truncatedList;
        }
    }

    private bool IsHighScore(int newScore)
    {
        if (highScoreData.highScores.Count < 10)
        {
            return true;
        }

        for (int i = 0; i < highScoreData.highScores.Count; ++i)
        {
            if (newScore > highScoreData.highScores.Get(i))
            {
                return true;
            }
        }
        return false;
    }

    private void SortHighScores()
    {
        List<int> scores = new List<int>();
        for (int i = 0; i < highScoreData.highScores.Count; ++i)
        {
            scores.Add(highScoreData.highScores.Get(i));
        }
        scores.Sort((a, b) => b.CompareTo(a));
        highScoreData.highScores = new SimplyLinkedList<int>();
        for (int i = 0; i < scores.Count; ++i)
        {
            highScoreData.highScores.InsertNodeAtEnd(scores[i]);
        }
    }
    public void BorrarTodo()
    {
        highScoreData.highScores = new SimplyLinkedList<int>();
        Debug.Log("Todos los puntajes han sido borrados.");
    }
}
