using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 5.0f;
    public float incrementSpeed = 0.01f;

    private Rigidbody rb;
    private KeyCode lastKeyPressed = KeyCode.RightArrow;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rb.velocity = new Vector2(speed, 0);
    }

    void Update()
    {
        // NOTE: ?????????????????????????????????
        //if (GameControl.Instance.IsGameOver()) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow) && lastKeyPressed != KeyCode.LeftArrow)
        {
            speed += incrementSpeed;
            SetVelocity(-speed);
            lastKeyPressed = KeyCode.LeftArrow;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && lastKeyPressed != KeyCode.RightArrow)
        {
            speed += incrementSpeed;
            SetVelocity(speed);
            lastKeyPressed = KeyCode.RightArrow;
        }
    }

    void SetVelocity(float xVelocity)
    {
        rb.velocity = new Vector2(xVelocity, 0);
    }
}
