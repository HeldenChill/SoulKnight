using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static EventManager Inst;

    [SerializeField]private GameObject environmentPrefab;
    [SerializeField]private RewardManager rewardManager;
    [SerializeField]private ButtonManager buttonManager;
    private void Awake(){
        if(Inst == null){
            Inst = this;
            Environment = Instantiate(environmentPrefab,transform.position,Quaternion.identity);
        }            
    }
    
    [HideInInspector]
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
        rewardManager.createNormalReward(position,level);
    }

    public void activePauseMenu(){
        buttonManager.setActivePauseGUI(true);
    }
}
