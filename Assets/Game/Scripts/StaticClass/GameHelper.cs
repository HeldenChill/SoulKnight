using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameHelper
{
    public static GameObject FindWeapon(GameObject gameObject){
        GameObject weapon = null;
        weapon = gameObject.GetComponentInChildren<Weapon>().gameObject;

        if(weapon == null){
            Debug.Log("No gun have found in " + gameObject.name);
        }
        return weapon;
    }

    public static Vector2Int RandomPosition(Vector2Int center,Vector2Int size){
        int valX = Random.Range(center.x - size.x,center.x + size.x + 1);
        int valY = Random.Range(center.y - size.y,center.y + size.y + 1);
        return new Vector2Int(valX,valY);
    }
    
}
