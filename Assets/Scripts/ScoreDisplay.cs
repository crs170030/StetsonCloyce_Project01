using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Text scoreText;

    public void ChangeUI(int score)
    {
        scoreText.text = "Score : " + score;
    }
}
