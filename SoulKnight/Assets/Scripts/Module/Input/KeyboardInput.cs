﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    private Vector2 moveKeyBoard = Vector2.zero; 
    private bool equipItem = false;
    private bool useSkill = false;
    void Update()
    {
        float valX = Input.GetAxisRaw("Horizontal");
        float valY = Input.GetAxisRaw("Vertical");
        equipItem = Input.GetKeyDown(KeyCode.E);
        useSkill = Input.GetKeyDown(KeyCode.Space);
        moveKeyBoard = new Vector2(valX,valY).normalized;
    }

    public Vector2 MoveKeyBoard{
        get{return moveKeyBoard;}
    }

    public bool EquipItem{
        get{return equipItem;}
    }

    public bool UseSkill{
        get{return useSkill;}
    }
}
