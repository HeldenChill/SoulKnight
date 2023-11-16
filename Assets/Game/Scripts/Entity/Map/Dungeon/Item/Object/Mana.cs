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
    public void GetItem(){
        if(contactPlayer.PlayerInRange[0] != null){
            contactPlayer.PlayerInRange[0].gameObject.GetComponent<PlayerBase>().Mana += 8;
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
