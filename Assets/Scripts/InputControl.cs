using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour
{
    public void OnLeftClick()
    {
        GameControl.Instance.ActionLeft();
    }

    public void OnRightClick()
    {
        GameControl.Instance.ActionRight();
    }
}
