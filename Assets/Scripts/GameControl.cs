using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    /// <summary>
    /// ブロックのプレハブ
    /// </summary>
    public GameObject blockObjectPrefab;

    public GameObject GameOverDialog;

    public Ball ball;

    public TextMeshProUGUI scoreBoard;
    public TextMeshProUGUI highScoreBoard;

    public int playerLife = 3;
    public int successCombo = 20;

    public AudioClip baseBGM;
    public AudioClip strongBGM;

    public AudioClip fadeSE;
    public AudioClip successSE;
    public AudioClip failureSE;

    private float positionY = -3.9f;
    private float minBlockWidth = 0.35f;

    private GameObject currentBlock;
    private KeyControl lastKeyPressed;

    private AudioSource audioSource;
    private AudioSource audioBGM;

    private int score;
    private int comboCount;

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
        lastKeyPressed = Keyboard.current.rightArrowKey;

        PlayBGM(baseBGM);

        currentBlock = Instantiate(blockObjectPrefab, CreateRandomVectorRight(), Quaternion.identity);
    }

    private void Update()
    {
        if (GameOverDialog.activeSelf) return;
        if (currentBlock == null) return;

        var leftKey = Keyboard.current.leftArrowKey;
        var rightKey = Keyboard.current.rightArrowKey;

        if (leftKey.wasPressedThisFrame )
        {
            ActionLeft();
        }
        else if (rightKey.wasPressedThisFrame )
        {
            ActionRight();
        }
    }

    // HACK: 重複しまくり
    public void ActionLeft()
    {
        var leftKey = Keyboard.current.leftArrowKey;
        if (lastKeyPressed == leftKey) return;

        if (!CheckOverlap())
        {
            if (playerLife == 0)
            {
                GameOver();
                return;
            }
            comboCount = 0;
            if (audioBGM.clip != baseBGM) PlayBGM(baseBGM);
            playerLife--;
            PlaySE(failureSE);
        }
        else
        {
            comboCount++;
            if (comboCount >= successCombo && audioBGM.clip != strongBGM) PlayBGM(strongBGM);
            PlaySE(successSE);
            ChangeScore();
            ChangeBlockWidth();
        }
        ball.MoveLeft();
        currentBlock.transform.position = CreateRandomVectorLeft();
        lastKeyPressed = leftKey;
    }

    // HACK: 重複しまくり
    public void ActionRight()
    {
        var rightKey = Keyboard.current.rightArrowKey;
        if (lastKeyPressed == rightKey) return;

        if (!CheckOverlap())
        {
            if (playerLife == 0)
            {
                GameOver();
                return;
            }
            comboCount = 0;
            if (audioBGM.clip != baseBGM) PlayBGM(baseBGM);
            playerLife--;
            PlaySE(failureSE);
        }
        else
        {
            comboCount++;
            if (comboCount >= 10 && audioBGM.clip != strongBGM) PlayBGM(strongBGM);
            PlaySE(successSE);
            ChangeScore();
            ChangeBlockWidth();
        }

        ball.MoveRight();
        currentBlock.transform.position = CreateRandomVectorRight();
        lastKeyPressed = rightKey;
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
        ScoreControl.Instance.SaveForHighScore(score);
        highScoreBoard.text = $"HIGH SCORE: {ScoreControl.Instance.HighScore}";
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
