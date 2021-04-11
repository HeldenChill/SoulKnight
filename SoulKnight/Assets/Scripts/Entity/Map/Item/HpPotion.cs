using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpPotion : MonoBehaviour,IItem
{
    private int value = 10;
    ContactPlayerModule contactPlayer;
    void Awake(){
        contactPlayer = gameObject.GetComponent<ContactPlayerModule>();
    }
    public void getItem(){
        if(contactPlayer.PlayerInRange[0] != null){
            contactPlayer.PlayerInRange[0].gameObject.GetComponent<PlayerBase>().getDamage(-value/5);
            Destroy(gameObject);
        }
        
    }
    public int getValue(){
        return value;
    }

    public void setActiveContact(bool isActive){
        contactPlayer.Active = isActive;
    }
}
