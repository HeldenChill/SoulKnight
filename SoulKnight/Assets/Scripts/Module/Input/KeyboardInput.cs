using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    private Vector2 moveKeyBoard = Vector2.zero; 
    void Update()
    {
        float valX = Input.GetAxisRaw("Horizontal");
        float valY = Input.GetAxisRaw("Vertical");
        moveKeyBoard = new Vector2(valX,valY).normalized;
    }

    public Vector2 MoveKeyBoard{
        get{return moveKeyBoard;}
    }
}
