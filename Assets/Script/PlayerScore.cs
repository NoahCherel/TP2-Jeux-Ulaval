using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    public int score = 0;
    public int currentScore { get; private set; }

    void Awake()
    {
        currentScore = score;
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        Debug.Log("Score ajouté ! Score actuel : " + currentScore);
    }
}
