using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class Ball : MonoBehaviour
{
    public float Speed = 5;
    public float incrementSpeed = 0.1f;

    private Rigidbody rb;
    private KeyControl lastKeyPressed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector2(Speed,  0);

        lastKeyPressed = Keyboard.current.rightArrowKey;
    }

    void Update()
    {
        var leftKey = Keyboard.current.leftArrowKey;
        var rightKey = Keyboard.current.rightArrowKey;

        if (leftKey.wasPressedThisFrame && lastKeyPressed != leftKey)
        {
            Speed += incrementSpeed;
            rb.velocity = new Vector2(-Speed, rb.velocity.y);
            lastKeyPressed = leftKey;
        }
        else if (rightKey.wasPressedThisFrame && lastKeyPressed != rightKey)
        {
            Speed += incrementSpeed;
            rb.velocity = new Vector2(Speed, rb.velocity.y);
            lastKeyPressed = rightKey;
        }
    }
}
