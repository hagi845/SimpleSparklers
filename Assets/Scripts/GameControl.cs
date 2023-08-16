using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: 責務が多すぎる このクラスは何者なのか不明
public class GameControl : MonoBehaviour
{
    private GameObject currentBlock;
    private KeyCode lastKeyPressed = KeyCode.RightArrow;
    private AudioManager backGroundMusic;
    private AudioManager sparkSound;

    public GameObject blockPrefab;
    public GameObject gameOverCanvas;
    public GameObject ball; // TODO: ブロック同様プレハブで生成する方が良いか

    public static GameControl Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        backGroundMusic = GameObject.Find("BackGroundMusic").GetComponent<AudioManager>();
        sparkSound = GameObject.Find("SparkSound").GetComponent<AudioManager>();
    }

    private void Start()
    {
        // 初期設定
        backGroundMusic.Play();
        currentBlock = Instantiate(blockPrefab, CreateRandomVectorRight(), Quaternion.identity);
    }

    private void Update()
    {   
        if (IsGameOver()) return;

        // TODO: 重複コード
        if (Input.GetKeyDown(KeyCode.LeftArrow) && lastKeyPressed != KeyCode.LeftArrow)
        {
            if (!CheckOverlap())
            {
                GameOver();
                return; 
            }
            sparkSound.Play();
            ChangeBlockWidth();
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
            sparkSound.Play();
            ChangeBlockWidth();
            currentBlock.transform.position = CreateRandomVectorRight();
            lastKeyPressed = KeyCode.RightArrow;
        }
    }

    private Vector2 CreateRandomVectorLeft() => CreateRandomVector(-6.5f, -1.5f);
    private Vector2 CreateRandomVectorRight() => CreateRandomVector(1.5f, 6.5f);

    /// <summary>
    /// 指定した範囲内でランダムなVectorを生成する
    /// </summary>
    /// <param name="minX"></param>
    /// <param name="maxX"></param>
    /// <returns></returns>
    private Vector2 CreateRandomVector(float minX, float maxX)
    {
        var randomX = Random.Range(minX, maxX);
        var positionY = -3.9f;
        return new Vector2(randomX, positionY);
    }

    /// <summary>
    /// ゲームオーバー
    /// TODO: ゲームオーバー単体のスクリプトにした方がいい？
    /// </summary>
    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
        backGroundMusic.Stop();
    }

    public bool IsGameOver() => gameOverCanvas.activeSelf;

    /// <summary>
    /// ボールとブロックが接触しているか
    /// TODO: 専用のスクリプトを作った方がいいのか
    /// </summary>
    /// <returns></returns>
    private bool CheckOverlap()
    {
        Bounds ballBounds = ball.GetComponent<Renderer>().bounds;
        Bounds barBounds = currentBlock.GetComponent<Renderer>().bounds;

        return ballBounds.Intersects(barBounds);
    }

    /// <summary>
    /// ブロックの幅を狭くする
    /// TODO: ブロック自身の責務？
    /// </summary>
    private void ChangeBlockWidth()
    {
        Vector2 currentScale = currentBlock.transform.localScale;
        var  minBlockWidth = 0.35f;

        if (currentScale.x <= minBlockWidth) return;
        currentScale.x -= 0.01f;
        currentBlock.transform.localScale = currentScale;
    }
}
