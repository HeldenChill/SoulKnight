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
    public void GetItem(){
        if(contactPlayer.PlayerInRange[0] != null){
            contactPlayer.PlayerInRange[0].gameObject.GetComponent<PlayerBase>().Money += 4;
            Destroy(gameObject);
        }   
    }
    public int GetValue(){
        return 0;
    }

    public void SetActiveContact(bool isActive){
        return;
    }
}
