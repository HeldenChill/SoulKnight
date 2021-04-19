using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static EventManager current;
    [SerializeField]private GameObject environmentPrefab;
    [SerializeField]private GameObject mana;
    [SerializeField]private GameObject coin;
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

    public void createNormalReward(Vector3 position,int level){
        int numOfObject = UnityEngine.Random.Range(0,level + 3);
        for(int i = 0; i < numOfObject; i++){
            Instantiate(mana,position,Quaternion.identity,Environment.transform);
            Instantiate(coin,position,Quaternion.identity,Environment.transform);
        }
    }

}
