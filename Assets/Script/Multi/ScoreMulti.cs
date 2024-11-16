using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;

public class ScoreMulti : NetworkBehaviour
{
    public int score = 0;
    public TMP_Text scoreText;

    public void AddScore(int amount)
    {
        if (IsServer)
        {
            score += amount;
        }
    }

    private void Update()
    {
        scoreText.text = "Score: " + score;
    }
}
