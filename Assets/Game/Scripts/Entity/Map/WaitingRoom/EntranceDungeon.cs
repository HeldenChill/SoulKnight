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
    public void SetActiveContact(bool isActive){
        contactPlayer.Active = isActive;
    }

    public void GetItem(){
        if(contactPlayer.PlayerInRange[0] != null){
            SetActiveContact(false);
            Scenes.current.Load("Pathfinding",WatingRoomManager.current.player);
            Scenes.current.playerProfile.setMaxAll();
            Debug.Log(Scenes.current.playerProfile.getInfo());
        }
    }

    

    public int GetValue(){return 0;}
}
