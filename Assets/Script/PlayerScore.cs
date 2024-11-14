using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    public int score = 0;
    public int currentScore { get; private set; }
    public int playerID;

    void Awake()
    {
        currentScore = score;
    }

    public void AddScore(int amount, int playerID)
    {
        currentScore += amount;
    }
}
