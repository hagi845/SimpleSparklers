using UnityEngine;

public class Ball : MonoBehaviour
{
    public float Speed = 5;
    public float incrementSpeed = 0.1f;

    private Rigidbody rb;
    private KeyCode lastKeyPressed = KeyCode.RightArrow;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector2(Speed,  0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && lastKeyPressed != KeyCode.LeftArrow)
        {
            Speed += incrementSpeed;
            rb.velocity = new Vector2(-Speed, rb.velocity.y);
            lastKeyPressed = KeyCode.LeftArrow;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && lastKeyPressed != KeyCode.RightArrow)
        {
            Speed += incrementSpeed;
            rb.velocity = new Vector2(Speed, rb.velocity.y);
            lastKeyPressed = KeyCode.RightArrow;
        }
    }
}
