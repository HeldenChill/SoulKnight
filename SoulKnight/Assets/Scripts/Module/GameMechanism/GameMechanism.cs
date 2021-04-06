using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameMechanism
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
    // Update is called once per frame
    
}
