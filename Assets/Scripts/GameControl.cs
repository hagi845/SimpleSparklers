using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    /// <summary>
    /// ブロックのプレハブ
    /// </summary>
    public GameObject blockObjectPrefab;

    public GameObject GameOverDialog;

    public GameObject ball;

    private float positionY = -3.9f;

    private GameObject currentBlock;
    private KeyCode lastKeyPressed = KeyCode.None;

    private AudioManager audioManager;

    public static GameControl Instance { get; private set; }

    private void Start()
    {
        Instance = this;

        var audio = GameObject.Find("Audio");
        if (audio != null)
        {
            audioManager = audio.GetComponent<AudioManager>();
        }

        currentBlock = Instantiate(blockObjectPrefab, CreateRandomVectorRight(), Quaternion.identity);
    }

    private void Update()
    {
        if (GameOverDialog.activeSelf) return;
        if (currentBlock == null) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow) && lastKeyPressed != KeyCode.LeftArrow)
        {
            if (!CheckOverlap())
            {
                GameOver();
                return; 
            }
            currentBlock.transform.position = CreateRandomVectorLeft();
            lastKeyPressed = KeyCode.LeftArrow;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && lastKeyPressed != KeyCode.RightArrow)
        {
            if (!CheckOverlap())
            {
                GameOver();
                return;
            }
            currentBlock.transform.position = CreateRandomVectorRight();
            lastKeyPressed = KeyCode.RightArrow;
        }
    }

    private Vector2 CreateRandomVectorLeft() => CreateRandomVector(-6.5f, -1.5f);
    private Vector2 CreateRandomVectorRight() => CreateRandomVector(1.5f, 6.5f);

    private Vector2 CreateRandomVector(float minX, float maxX)
    {
        float randomX = Random.Range(minX, maxX);
        return new Vector2(randomX, positionY);
    }

    public void GameOver()
    {
        GameOverDialog.SetActive(true);
        audioManager.StopBackgroundMusic();
    }

    private bool CheckOverlap()
    {
        Bounds ballBounds = ball.GetComponent<Renderer>().bounds;
        Bounds barBounds = currentBlock.GetComponent<Renderer>().bounds;

        return ballBounds.Intersects(barBounds);
    }

}
