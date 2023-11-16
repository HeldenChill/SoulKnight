using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour,IItem
{
    
    ContactPlayerModule contactPlayer;
    void Start()
    {
        contactPlayer = GetComponent<ContactPlayerModule>();
    }
    public void SetActiveContact(bool isActive){
        contactPlayer.Active = isActive;
    }

    public void GetItem(){
        if(contactPlayer.PlayerInRange[0] != null){
            SetActiveContact(false);
            EventManager.Inst.EndArea();
        }
    }

    

    public int GetValue(){return 0;}
    
}
