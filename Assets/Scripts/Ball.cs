using UnityEngine;

public class Ball : MonoBehaviour
{
    public float Speed = 5;
    public float incrementSpeed = 0.01f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector2(Speed,  0);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Speed += incrementSpeed;
            rb.velocity = new Vector2(-Speed, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Speed += incrementSpeed;
            rb.velocity = new Vector2(Speed, rb.velocity.y);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // TODO: 
        Debug.Log("gameover???");
    }
}
