using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : FollowTarget,IItem
{
    ContactPlayerModule contactPlayer;
    protected override void Start()
    {
        base.Start();
        contactPlayer = GetComponent<ContactPlayerModule>();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public void getItem(){
        if(contactPlayer.PlayerInRange[0] != null){
            contactPlayer.PlayerInRange[0].gameObject.GetComponent<PlayerBase>().Money += 4;
            Destroy(gameObject);
        }   
    }
    public int getValue(){
        return 0;
    }

    public void setActiveContact(bool isActive){
        return;
    }
}
