using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameControl.Instance.GameOver();
    }
}
