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
    public void GetItem(){
        if(contactPlayer.PlayerInRange[0] != null){
            contactPlayer.PlayerInRange[0].gameObject.GetComponent<PlayerBase>().Hp += 4;
            Destroy(gameObject);
        }
        
    }
    public int GetValue(){
        return value;
    }

    public void SetActiveContact(bool isActive){
        contactPlayer.Active = isActive;
    }
}
