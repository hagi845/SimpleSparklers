using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartControl : MonoBehaviour
{
    private AudioManager bgm;

    public TextMeshProUGUI highScoreBoard;

    private void Start()
    {
        var background = GameObject.Find("BGM");
        if (background != null) bgm = background.GetComponent<AudioSource>().GetComponent<AudioManager>();
        bgm.Play();

        highScoreBoard.text = $"HIGH SCORE: {ScoreControl.Instance.HighScore}" ;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
