using UnityEngine;
using TMPro;
using System;

public class PlayerScore : MonoBehaviour
{
    int score = 0;

    public int minScore = 3;
    public TMP_Text scoreText;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
        if (score < minScore)
        {
            scoreText.enabled = false;
        } 
        else
        {
            scoreText.enabled = true;
        }
    }

    public void IncreaseScore()
    {
        score++;
    }

    public void ResetScore()
    {
        if (score > GameController.maxCombo)
        {
            GameController.maxCombo = score;
        }
        score = 0;
    }
}
