using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 5.0f;
    public float incrementSpeed = 0.01f;

    private Rigidbody rb;

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
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            speed += incrementSpeed;
            SetVelocity(-speed);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            speed += incrementSpeed;
            SetVelocity(speed);
        }
    }

    void SetVelocity(float xVelocity)
    {
        rb.velocity = new Vector2(xVelocity, 0);
    }
}
