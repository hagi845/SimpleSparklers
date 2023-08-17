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

    public AudioClip baseBGM;
    public AudioClip strongBGM;

    public AudioClip fadeSE;
    public AudioClip successSE;
    public AudioClip failureSE;

    private float positionY = -3.9f;
    private float minBlockWidth = 0.35f;

    private GameObject currentBlock;
    private KeyCode lastKeyPressed = KeyCode.RightArrow;

    private AudioSource audioSource;
    private AudioSource audioBGM;

    private long score;

    public static GameControl Instance { get; private set; }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioBGM = GetComponent<AudioSource>();
        audioBGM.loop = true;
    }

    private void Start()
    {
        Instance = this;

        PlayBGM(baseBGM);

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
                PlaySE(failureSE);
            }
            else
            {
                PlaySE(successSE);
                ChangeScore();
                ChangeBlockWidth();
            }

            currentBlock.transform.position = CreateRandomVectorLeft();
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
                PlaySE(failureSE);
            }
            else
            {
                PlaySE(successSE);
                ChangeScore();
                ChangeBlockWidth();
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
        if (GameOverDialog.activeSelf) return;
        GameOverDialog.SetActive(true);
        audioBGM.Stop();
        PlaySE(fadeSE);
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

    private void PlaySE(AudioClip audioClip)
    {
        if (audioSource == null) return;
        audioSource.PlayOneShot(audioClip);
    }

    private void PlayBGM(AudioClip audioClip)
    {
        if (audioBGM == null) return;
        if (audioBGM.isPlaying) audioBGM.Stop();
        audioBGM.clip = audioClip;
        audioBGM.Play();
    }
}
