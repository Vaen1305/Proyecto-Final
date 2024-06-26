using UnityEngine;

[CreateAssetMenu(fileName = "HighScoreData", menuName = "ScriptableObjects/HighScoreData", order = 1)]
public class HighScoreData : ScriptableObject
{
    public SimplyLinkedList<int> highScores = new SimplyLinkedList<int>();
}
