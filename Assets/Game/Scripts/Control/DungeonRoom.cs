﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TypeOfRoom
{
        Start = 0,
        Monster = 1,
        Shop = 2,
        Chest = 3,
        Boss = 4,
        Portal = 5

    }
public class DungeonRoom : MonoBehaviour
{
    
    // attribute:
    /*  -sizeOfRoom
        -position
        -strengthStatus
        -typeOfRoom
        -EnemyInRoom
        -ShopInRoom
        -PortalInRoom
    */
    private static int makeId = 0;
    public int id;
    public int numOfEnemy;
    public Vector2Int size;
    public Vector2Int position;
    public Vector2Int gridPosition;
    public BoxCollider2D area = null;
    public int strengthStatus;
    public TypeOfRoom type;
    public bool[] haveLobby = new bool[4];
    public List<GameObject> functionalObj;
    private List<Enemy> enemies;
    private Shop shop;
    private Portal portal;


    //behaviour:
    /*  -Init()
        -createRoom()
        -playerEnterRoom()
        -trigger()
        -takeMoneyFromCharacter()
        -enemyDie()
        -winRoom()
        -winFloor()
        -characterEnterNewArea()
    */
    void Awake(){
        id = makeId;
        makeId += 1;
        EventManager.Inst.onEnemyDie += EnemyDie;
    }
    void OnDestroy(){
        EventManager.Inst.onEnemyDie -= EnemyDie;
    }
    void FixedUpdate(){
        if(area != null){
            if(area.IsTouchingLayers(OrangePlayer.layerMask)){
                CloseRoom();
                triggerEnemy();
                area.enabled = false;
                area = null;
            }
        }
    }
    public void Init(Vector2Int baseSize,Vector2Int position,int strengthStatus,TypeOfRoom type,List<GameObject> functionalObj){
        this.size = baseSize;
        this.position = position;
        this.strengthStatus = strengthStatus;
        this.type = type;
        this.functionalObj = functionalObj;
        int addSize = 0;
        //Debug.Log(haveLobby[2]);
        if(this.type == TypeOfRoom.Monster){
            addSize = Random.Range(7,12);
            size += new Vector2Int(addSize,addSize);
            area = gameObject.AddComponent<BoxCollider2D>();
            area.isTrigger = true;
        }
        else if(this.type == TypeOfRoom.Boss){
            size += new Vector2Int(15,15);    
        }
        else if(this.type == TypeOfRoom.Shop){
            addSize = Random.Range(10,12);
            size += new Vector2Int(addSize,addSize-4);
        }
        else if(this.type == TypeOfRoom.Chest){
            size += new Vector2Int(2,2);
        }
        else if(this.type == TypeOfRoom.Portal){
            size += new Vector2Int(2,2);
        }
    }



    public void CreateDetailRoom(CreateMap map,List<GameObject> decoration){
        gridPosition = map.DrawRoom(position,size,decoration);
        if(type == TypeOfRoom.Monster || type == TypeOfRoom.Boss){
            functionalObj = map.CreateMosterRoom(this);
            OpenRoom();
        }
        else if(type == TypeOfRoom.Chest){
            functionalObj = map.CreateChestRoom(this);
        }
        else if(type == TypeOfRoom.Shop){
            functionalObj = map.CreateShopRoom(this);
        }
        else if(type == TypeOfRoom.Portal){
            functionalObj = map.CreatePortalRoom(this);
        }
    }

    public void OpenRoom(){
        foreach (var door in functionalObj)
        {
            door.SetActive(false);           
        }
    }

    public void CloseRoom(){
        foreach (var door in functionalObj)
        {
            if(door != null)
                door.SetActive(true);           
        }
    }
    private void WinRoom(){
        OpenRoom();
    }
    private void triggerEnemy(){
        EventManager.Inst.PlayerEnterMonsterRoom(id);
    }

    private void EnemyDie(int id){
        if(this.id == id){
            numOfEnemy -= 1;
            if(numOfEnemy <= 0){
                WinRoom();
            }
        }
        
    }
    

}
