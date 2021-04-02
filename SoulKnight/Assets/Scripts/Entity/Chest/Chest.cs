using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    List<GameObject> items;
    void Init(List<GameObject> items){
        this.items = items;
    }


    public void open(){
        if(items != null){
            int indexItem = Random.Range(0,items.Count);
            createItem(items[indexItem]);
        }
    }

    private void createItem(GameObject item){
        if(item != null){
            Instantiate(item,this.transform.position,Quaternion.identity);
        }
        else{
            Debug.Log("null items");
        }   
    }
}
