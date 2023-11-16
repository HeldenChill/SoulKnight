using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputModule : MonoBehaviour
{
    private Vector2 moveKeyBoard = Vector2.zero; 
    private bool equipItem = false;
    private bool useSkill = false;
    private bool optionMenu = false;
    void Update()
    {
        float valX = Input.GetAxisRaw("Horizontal");
        float valY = Input.GetAxisRaw("Vertical");
        equipItem = Input.GetKeyDown(KeyCode.E);
        useSkill = Input.GetKeyDown(KeyCode.Space);
        optionMenu = Input.GetKeyDown(KeyCode.Escape);
        moveKeyBoard = new Vector2(valX,valY);        
    }

    public Vector2 MoveKeyBoard{
        get{return moveKeyBoard.normalized;}
    }

    public bool EquipItem{
        get{return equipItem;}
    }

    public bool UseSkill{
        get{return useSkill;}
    }

    public bool OptionMenu{
        get{return optionMenu;}
    }
}
