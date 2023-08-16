using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    /// <summary>
    /// ブロックのプレハブ
    /// </summary>
    public GameObject blockObjectPrefab;

    public GameObject GameOverDialog;

    public GameObject ball;

    public TextMeshProUGUI scoreBoard;

    public int playerLife = 3;

    private float positionY = -3.9f;
    private float minBlockWidth = 0.35f;

    private GameObject currentBlock;
    private KeyCode lastKeyPressed = KeyCode.RightArrow;

    private AudioManager backGroundMusic;
    private AudioManager sparkSound;
    private AudioManager missSound;

    private long score;

    public static GameControl Instance { get; private set; }

    private void Start()
    {
        Instance = this;

        var background = GameObject.Find("BackGroundMusic");
        if (background != null) backGroundMusic = background.GetComponent<AudioSource>().GetComponent<AudioManager>();
        backGroundMusic.Play();

        var spark = GameObject.Find("SparkSound");
        if (spark != null) sparkSound = spark.GetComponent<AudioManager>();

        var miss = GameObject.Find("MissSound");
        if (miss != null) missSound = miss.GetComponent<AudioManager>();

        currentBlock = Instantiate(blockObjectPrefab, CreateRandomVectorRight(), Quaternion.identity);
    }

    private void Update()
    {
        if (GameOverDialog.activeSelf) return;
        if (currentBlock == null) return;

        // HACK: 重複しまくり
        if (Input.GetKeyDown(KeyCode.LeftArrow) && lastKeyPressed != KeyCode.LeftArrow)
        {
            if (!CheckOverlap())
            {
                if(playerLife == 0)
                {
                    GameOver();
                    return;
                }
                playerLife--;
                missSound.Play();
            }
            else
            {
                sparkSound.Play();
            }

            ChangeBlockWidth();
            currentBlock.transform.position = CreateRandomVectorLeft();
            ChangeScore();
            lastKeyPressed = KeyCode.LeftArrow;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && lastKeyPressed != KeyCode.RightArrow)
        {
            if (!CheckOverlap())
            {
                if (playerLife == 0)
                {
                    GameOver();
                    return;
                }
                playerLife--;
                missSound.Play();
            }
            else
            {
                sparkSound.Play();
            }

            ChangeBlockWidth();
            currentBlock.transform.position = CreateRandomVectorRight();
            ChangeScore();
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
        backGroundMusic.Stop();
    }

    private bool CheckOverlap()
    {
        Bounds ballBounds = ball.GetComponent<Renderer>().bounds;
        Bounds barBounds = currentBlock.GetComponent<Renderer>().bounds;

        return ballBounds.Intersects(barBounds);
    }

    private void ChangeBlockWidth()
    {
        Vector2 currentScale = currentBlock.transform.localScale;

        if (currentScale.x <= minBlockWidth) return;
        currentScale.x -= 0.01f;
        currentBlock.transform.localScale = currentScale;
    }

    private void ChangeScore()
    {
        score += 100;
        scoreBoard.text = score.ToString();
    }
}
