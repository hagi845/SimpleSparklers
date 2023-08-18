using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Ball : MonoBehaviour
{
    public float Speed = 5;
    public float incrementSpeed = 0.1f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector2(Speed,  0);
    }

    public void MoveLeft()
    {
        Speed += incrementSpeed;
        rb.velocity = new Vector2(-Speed, rb.velocity.y);
    }

    public void MoveRight()
    {
        Speed += incrementSpeed;
        rb.velocity = new Vector2(Speed, rb.velocity.y);
    }

}
