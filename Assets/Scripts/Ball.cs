using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float Speed = 5;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(Speed, 0, 0, ForceMode.VelocityChange);
    }

    void Update()
    {
        
    }
}
