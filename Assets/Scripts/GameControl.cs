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

    private float positionY = -3.9f;
    private float positionZ = 2.0f;

    private GameObject currentBlock;
    private KeyCode lastKeyPressed = KeyCode.None;

    public static GameControl Instance { get; private set; }

    private void Start()
    {
        Instance = this;
        currentBlock = Instantiate(blockObjectPrefab, CreateRandomVectorRight(), Quaternion.identity);
    }

    private void Update()
    {
        if (currentBlock == null) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow) && lastKeyPressed != KeyCode.LeftArrow)
        {
            currentBlock.transform.position = CreateRandomVectorLeft();
            lastKeyPressed = KeyCode.LeftArrow;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && lastKeyPressed != KeyCode.RightArrow)
        {
            currentBlock.transform.position = CreateRandomVectorRight();
            lastKeyPressed = KeyCode.RightArrow;
        }
    }

    private Vector3 CreateRandomVectorLeft() => CreateRandomVector(-6.5f, -1.5f);
    private Vector3 CreateRandomVectorRight() => CreateRandomVector(1.5f, 6.5f);

    private Vector3 CreateRandomVector(float minX, float maxX)
    {
        float randomX = Random.Range(minX, maxX);
        return new Vector3(randomX, positionY, positionZ);
    }

    public void GameOver()
    {
        GameOverDialog.SetActive(true);
    }
}
