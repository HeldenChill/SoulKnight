using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceDungeon : MonoBehaviour,IItem
{
    // Start is called before the first frame update
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
            Scenes.current.Load("GameScene",WatingRoomManager.current.player);
            Scenes.current.playerProfile.setMaxAll();
            Debug.Log(Scenes.current.playerProfile.getInfo());
        }
    }

    

    public int getValue(){return 0;}
}
