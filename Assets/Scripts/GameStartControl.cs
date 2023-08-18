using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartControl : MonoBehaviour
{
    private AudioManager bgm;

    private void Start()
    {
        var background = GameObject.Find("BGM");
        if (background != null) bgm = background.GetComponent<AudioSource>().GetComponent<AudioManager>();
        bgm.Play();


        Debug.Log("gamestart highscore: " + ScoreControl.Instance.HighScore);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
