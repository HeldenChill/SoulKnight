using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static EventManager current;
    [SerializeField]private GameObject environmentPrefab;
    private void Awake(){
        if(current == null){
            current = this;
            Environment = Instantiate(environmentPrefab,transform.position,Quaternion.identity);
        }
            
    }
    
    public GameObject Environment;
    public event Action<int> onPlayerEnterMonsterRoom;
    public event Action<int> onEnemyDie;

    public event Action onEndArea;
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

    public void EndArea(){
        if(onEndArea != null){
            Destroy(Environment);
            Environment = Instantiate(environmentPrefab,transform.position,Quaternion.identity);
            onEndArea();
            PlayerBase.player.GetComponent<PlayerBase>().resetPosition();
        }
    }

}
