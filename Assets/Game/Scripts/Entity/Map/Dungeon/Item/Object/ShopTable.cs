using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTable : MonoBehaviour,IItem
{
    public int idTable = -1;
    private int value = 0;
    ContactPlayerModule contactPlayer;
    void Awake(){
        contactPlayer = GetComponent<ContactPlayerModule>();
    }
    public void SetActiveContact(bool isActive){
        contactPlayer.Active = isActive;
    }
    public void GetItem(){
        Shop shop = transform.parent.GetComponent<Shop>();
        GameObject player = null;
        if(contactPlayer.PlayerInRange[0] != null){
            player = contactPlayer.PlayerInRange[0].gameObject;
        }
            
        shop.playerBuyItem(idTable,player);
    }
    public int GetValue(){
        return value;
    }
}
