using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour,IItem
{
    public List<GameObject> items;
    ContactPlayerModule contactPlayer;

    void Start(){
        contactPlayer = gameObject.GetComponent<ContactPlayerModule>();
    }
    void Init(List<GameObject> items){
        this.items = items;
    }


    
    public void setActiveContact(bool isActive){
        contactPlayer.Active = isActive;
    }

    public void getItem(){
        if(contactPlayer.PlayerInRange[0] != null){
            open();
            setActiveContact(false);
        }
        
    }

    public int getValue(){return 0;}

    private void open(){
        if(items != null){
            int indexItem = Random.Range(0,items.Count);
            createItem(items[indexItem]);
        }
    }

    private void createItem(GameObject item){
        if(item != null){
            GameObject obj = Instantiate(item,this.transform.position,Quaternion.identity,EventManager.current.Environment.transform);
            obj.GetComponent<IItem>().setActiveContact(true);
        }
        else{
            Debug.Log("null items");
        }   
    }
}
