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
    public void setActiveContact(bool isActive){
        contactPlayer.Active = isActive;
    }

    public void getItem(){
        if(contactPlayer.PlayerInRange[0] != null){
            setActiveContact(false);
            EventManager.current.EndArea();
        }
    }

    

    public int getValue(){return 0;}
    
}
