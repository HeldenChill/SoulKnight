﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonFloor : MonoBehaviour
{   
    //atrribute
    /*  -level
        -positionOfRoom
        -numberOfArea
    */
    int level;
    int[,] positionOfRoom;
    Vector2Int rootCoordinates;
    Vector2Int baseSizeOfRoom = new Vector2Int(8,8);
    int numberOfArea;
    public CreateMap map;
    public int numberOfRoom;
    private int[] numberOfRoomType;
    //object
    public List<DungeonRoom> rooms;
    public List<GameObject> decoration;
    private List<List<GameObject>> funcObj;
    public List<GameObject> startObj;
    public List<GameObject> monsterObj;
    public List<GameObject> shopObj;
    
    public List<GameObject> chestObj;
    public List<GameObject> bossObj;
    public List<GameObject> portalObj;
    
    //behaviour
    /*  -Init()
        -createMap()
        -createEnemy()
        -chracterEnterNewFloor()
        -chracterEnterNewArea()
    */
    private void initFuncObj(){
        funcObj = new List<List<GameObject>>();
        funcObj.Add(startObj);
        funcObj.Add(monsterObj);
        funcObj.Add(shopObj);
        funcObj.Add(chestObj);
        funcObj.Add(bossObj);
        funcObj.Add(portalObj);

    }
    public void Start(){
        rooms = new List<DungeonRoom>();
        map = gameObject.GetComponent<CreateMap>();
        Init();
        initFuncObj();
        createSketchMap();
        createDetailMap();
    }
    public void Init(int level = 1){
        this.level = level;
        positionOfRoom = new int[level+6,level+6];
        numberOfArea = level + 3;
        numberOfRoom = 0;
        numberOfRoomType = new int[6];

        for(int i = 0; i <= positionOfRoom.GetUpperBound(0); i++){
            for(int j = 0; j <= positionOfRoom.GetUpperBound(1); j++){
                positionOfRoom[i,j] = -1;
            }
        }

        numberOfRoomType[0] = 1;
        numberOfRoomType[1] = Random.Range(level+2,level+4);
        numberOfRoomType[2] = Random.Range(level-1,level+1);
        numberOfRoomType[3] = Random.Range(level,level+2);
        if(numberOfArea == 0){
            numberOfRoomType[4] = 1;
            numberOfRoomType[5] = 0;
        }
        else{
            numberOfRoomType[4] = 0;
            numberOfRoomType[5] = 1;
        }
        
        
        for(int i = 0; i < 6; i++){
            numberOfRoom += numberOfRoomType[i];
        }
    }

    public void createSketchMap(){
        int x = Random.Range(1,positionOfRoom.GetUpperBound(0));
        int y = Random.Range(1,positionOfRoom.GetUpperBound(1));

        rootCoordinates = new Vector2Int(x,y);
        positionOfRoom[x,y] = 0;
        numberOfRoomType[0] -= 1;
        DungeonRoom room = gameObject.AddComponent<DungeonRoom>();
        room.Init(baseSizeOfRoom,new Vector2Int(0,0),level,0,funcObj[0]);
        rooms.Add(room);

        for(int i = 1; i < 6;i++){
            while(numberOfRoomType[i] > 0){
                x = Random.Range(1,positionOfRoom.GetUpperBound(0));
                y = Random.Range(1,positionOfRoom.GetUpperBound(1));
                if(positionOfRoom[x,y] != -1){
                    continue;
                }

                if(positionOfRoom[x+1,y] == -1 && positionOfRoom[x-1,y] == -1 
                && positionOfRoom[x,y+1] == -1 && positionOfRoom[x,y-1] == -1){
                    continue;
                }
                else{
                    if(i < 4){
                        positionOfRoom[x,y] = i;
                        room = gameObject.AddComponent<DungeonRoom>();
                        
                    }
                    else{
                        if(Mathf.Abs(x-rootCoordinates.x) + Mathf.Abs(y-rootCoordinates.y) > 1.5){
                            positionOfRoom[x,y] = i;
                            room = gameObject.AddComponent<DungeonRoom>();
                        }
                    }
                    room.Init(new Vector2Int(5,5),new Vector2Int(x,y) - rootCoordinates,level,(TypeOfRoom)i,funcObj[i]);
                    rooms.Add(room);
                    numberOfRoomType[i] -= 1;
                }
            }
        }
        updateLobbyForRoom();
    }

    public void createDetailMap(){
        
        foreach (var item in rooms)
        {
            item.createDetailRoom(map,decoration);
            createLobby(item);
        }
        
    }

    public void chracterEnterNewArea(){

    }
    public void chracterEnterNewFloor(){

    }
    private void updateLobbyForRoom(){
        foreach(var room in rooms){
            Vector2Int pos = room.position + rootCoordinates;
            if(positionOfRoom[pos.x + 1,pos.y] != - 1){
                room.haveLobby[0] = true;
            }

            if(positionOfRoom[pos.x - 1,pos.y] != - 1){
                room.haveLobby[2] = true;
            }

            if(positionOfRoom[pos.x,pos.y + 1] != - 1){
                room.haveLobby[1] = true;
            }

            if(positionOfRoom[pos.x,pos.y - 1] != - 1){
                room.haveLobby[3] = true;
            }
        }
    }
    private void createLobby(DungeonRoom room){
        Vector2Int pos = room.position + rootCoordinates;
        if(room.haveLobby[0]){
            map.drawLobby((Vector3Int)room.position ,new Vector3Int(pos.x + 1,pos.y,0)-(Vector3Int)rootCoordinates);
        }

        if(room.haveLobby[2]){
            map.drawLobby((Vector3Int)room.position ,new Vector3Int(pos.x - 1,pos.y,0)-(Vector3Int)rootCoordinates);
        }

        if(room.haveLobby[1]){
            map.drawLobby((Vector3Int)room.position ,new Vector3Int(pos.x,pos.y + 1,0)-(Vector3Int)rootCoordinates);
        }

        if(room.haveLobby[3]){
            map.drawLobby((Vector3Int)room.position ,new Vector3Int(pos.x ,pos.y - 1,0)-(Vector3Int)rootCoordinates);
        }
    }
}
