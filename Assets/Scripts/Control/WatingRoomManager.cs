using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatingRoomManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static WatingRoomManager current;
    public GameObject player;
    private void Awake(){
        if(current == null){
            current = this;
        }
    }

    private void Start(){
        player = PlayerBase.player;
    }
}
