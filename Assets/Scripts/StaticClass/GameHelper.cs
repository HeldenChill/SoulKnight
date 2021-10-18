using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameHelper
{
    public static GameObject findWeapon(GameObject gameObject){
        GameObject weapon = null;
        for(int i = 0; i < gameObject.transform.childCount; i++){
            if(gameObject.transform.GetChild(i).TryGetComponent<Weapon>(out Weapon component)){
                weapon = gameObject.transform.GetChild(i).gameObject;
                break;
            }
        }

        if(weapon == null){
            Debug.Log("No gun have found in " + gameObject.name);
        }
        return weapon;
    }

    public static Vector2Int pickRandomPosition(Vector2Int center,Vector2Int size){
        int valX = Random.Range(center.x - size.x,center.x + size.x + 1);
        int valY = Random.Range(center.y - size.y,center.y + size.y + 1);
        return new Vector2Int(valX,valY);
    }
    
}
