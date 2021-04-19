using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : FollowTarget,IItem
{
    // Start is called before the first frame update
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
            contactPlayer.PlayerInRange[0].gameObject.GetComponent<PlayerBase>().Mana += 8;
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
