using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static EventManager current;
    private void Awake(){
        if(current == null)
            current = this;
    }

    public event Action<int> onPlayerEnterMonsterRoom;
    public event Action<int> onEnemyDie;
    public void PlayerEnterMonsterRoom(int id){
        if(onPlayerEnterMonsterRoom != null){
            onPlayerEnterMonsterRoom(id);
        }
    }

    public void EnemyDie(int id){
        if(onEnemyDie != null){
            onEnemyDie(id);
        }
    }
}
