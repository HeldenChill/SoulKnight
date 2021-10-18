using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CreateMap : MonoBehaviour
{
    public RuleTile tile;
    public Tilemap worldTile;
    
    int lengthOfLobby = 25;
    int widthOfLobby = 3;
    void Start(){
        createMap();
    }
    void createMap(){
        //draw room
        //drawRoom(new Vector3Int(1,3,0),new Vector3Int(6,6,0));
        //drawRectangleTile(/*(Vector3Int)item.position*/new Vector3Int(30,90,0),6,6);
        
    }

    private void drawRectangleTile(Vector3Int position,int width,int height){
        for(int i = position.x - width; i <= position.x + width; i++){
            for(int j = position.y - height; j <= position.y + height; j++){
                worldTile.SetTile(new Vector3Int(i,j,0),tile);
            }
        }
    }

    public void drawLobby(Vector3Int pos1,Vector3Int pos2){
        Vector3Int direction = pos1 - pos2;
        Vector3Int drawPoint = (pos1 + pos2) * lengthOfLobby;

        int width;
        int height;
        //Debug.Log(direction);
        if(direction.x != 0){
            width = lengthOfLobby;
            height = widthOfLobby;
        }
        else{
            width = widthOfLobby;
            height = lengthOfLobby;
            //Debug.Log("Height");
        }

        drawRectangleTile(drawPoint,width,height);
    }


    public Vector2Int drawRoom(Vector2Int position,Vector2Int size,List<GameObject> decoration){
        Vector2Int gridPosition = position*lengthOfLobby*2;
        drawRectangleTile((Vector3Int)gridPosition,size.x,size.y);
        Instantiate(decoration[1],(Vector3)globalPosition(gridPosition + new Vector2Int(7,7)),Quaternion.identity,EventManager.current.Environment.transform);
        Instantiate(decoration[1],(Vector3)globalPosition(gridPosition + new Vector2Int(7,-6)),Quaternion.identity,EventManager.current.Environment.transform);
        Instantiate(decoration[1],(Vector3)globalPosition(gridPosition + new Vector2Int(-7,7)),Quaternion.identity,EventManager.current.Environment.transform);
        Instantiate(decoration[1],(Vector3)globalPosition(gridPosition + new Vector2Int(-7,-6)),Quaternion.identity,EventManager.current.Environment.transform);
        return (Vector2Int) gridPosition;
    }

    public List<GameObject> createMosterRoom(DungeonRoom room){
        //draw door
        List<GameObject> doors = new List<GameObject>();
        if(room.haveLobby[1] == true){
            for(int i = -widthOfLobby; i <= widthOfLobby; i++){
                Vector2 positionOfDoor = globalPosition(room.gridPosition + new Vector2Int(i,room.size.y));
                doors.Add(Instantiate(room.functionalObj[0],(Vector3)positionOfDoor,Quaternion.identity,EventManager.current.Environment.transform));
            }
        }
        
        if(room.haveLobby[3] == true){
            for(int i = -widthOfLobby; i <= widthOfLobby; i++){
                Vector2 positionOfDoor = globalPosition(room.gridPosition + new Vector2Int(i,-room.size.y));
                doors.Add(Instantiate(room.functionalObj[0],(Vector3)positionOfDoor,Quaternion.identity,EventManager.current.Environment.transform));
            }
        }
        
        if(room.haveLobby[0] == true){
            for(int i = -widthOfLobby; i <= widthOfLobby; i++){
                Vector2 positionOfDoor = globalPosition(room.gridPosition + new Vector2Int(room.size.x,i));
                doors.Add(Instantiate(room.functionalObj[0],(Vector3)positionOfDoor,Quaternion.identity,EventManager.current.Environment.transform));
            }
        }
        
        if(room.haveLobby[2] == true){
            for(int i = -widthOfLobby; i <= widthOfLobby; i++){
                Vector2 positionOfDoor = globalPosition(room.gridPosition + new Vector2Int(-room.size.x,i));
                doors.Add(Instantiate(room.functionalObj[0],(Vector3)positionOfDoor,Quaternion.identity,EventManager.current.Environment.transform));
            }
        }     
        // set manager area
        room.area.offset = new Vector2(0.08f,0.16f) + ((Vector2)room.gridPosition) *0.16f;
        room.area.size = (Vector2)room.size * 0.16f * 2 - new Vector2(0.16f,0.32f)*1.5f;
        return doors;
    }

    public List<GameObject> createChestRoom(DungeonRoom room){
        List<GameObject> chests = new List<GameObject>();
        Vector3 positionOfChest = (Vector3)globalPosition(room.gridPosition);
        chests.Add(Instantiate(room.functionalObj[0],positionOfChest,Quaternion.identity,EventManager.current.Environment.transform));
        return chests;
    }

    public List<GameObject> createShopRoom(DungeonRoom room){
        List<GameObject> shopsObj = new List<GameObject>();
        Vector3 positionOfShop = (Vector3)globalPosition(room.gridPosition) + new Vector3(0,worldTile.cellSize.x * gameObject.transform.localScale.x * 4,0) ;
        shopsObj.Add(Instantiate(room.functionalObj[0],positionOfShop,Quaternion.identity,EventManager.current.Environment.transform));
        return shopsObj;
    }

    public List<GameObject> createPortalRoom(DungeonRoom room){
        List<GameObject> portalsObj = new List<GameObject>();
        Vector3 positionOfPortal = (Vector3)globalPosition(room.gridPosition) ;
        portalsObj.Add(Instantiate(room.functionalObj[0],positionOfPortal,Quaternion.identity,EventManager.current.Environment.transform));
        return portalsObj;
    }

    public Vector2 globalPosition(Vector2Int postion){
        return worldTile.CellToWorld((Vector3Int)postion) + worldTile.cellSize * gameObject.transform.localScale.x / 2;
    }

    public void clearMap(){
        worldTile.ClearAllTiles();
    }
}
