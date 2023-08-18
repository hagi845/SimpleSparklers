using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreControl : MonoBehaviour
{
    public static ScoreControl Instance { get; private set; }

    public int HighScore { get; private set; }

    void Start()
    {
        Instance = this;
        HighScore = PlayerPrefs.GetInt("score");
    }

    public void SaveForHighScore(int score)
    {
        if (score < HighScore) return;  
        Save(score);
        HighScore = score;
    }

    void Save(int score)
    {
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.Save();
    }
}
