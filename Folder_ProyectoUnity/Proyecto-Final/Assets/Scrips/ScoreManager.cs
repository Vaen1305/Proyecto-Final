using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] scoreTexts;

    void Start()
    {
        SimplyLinkedList<int> highScores = GameManager.Instance.GetHighScores();
        DisplayScores(highScores);
    }

    private void DisplayScores(SimplyLinkedList<int> scores)
    {
        for (int i = 0; i < scoreTexts.Length; ++i)
        {
            if (i < scores.Count)
            {
                scoreTexts[i].text = (i + 1) + ". " + scores.Get(i);
            }
            else
            {
                scoreTexts[i].text = (i + 1) + ". -";
            }
        }
    }
}
