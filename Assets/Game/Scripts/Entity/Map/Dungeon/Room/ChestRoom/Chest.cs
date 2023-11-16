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


    
    public void SetActiveContact(bool isActive){
        contactPlayer.Active = isActive;
    }

    public void GetItem(){
        if(contactPlayer.PlayerInRange[0] != null){
            open();
            SetActiveContact(false);
        }
        
    }

    public int GetValue(){return 0;}

    private void open(){
        if(items != null){
            int indexItem = Random.Range(0,items.Count);
            createItem(items[indexItem]);
        }
    }

    private void createItem(GameObject item){
        if(item != null){
            GameObject obj = Instantiate(item,this.transform.position,Quaternion.identity,EventManager.Inst.Environment.transform);
            obj.GetComponent<IItem>().SetActiveContact(true);
        }
        else{
            Debug.Log("null items");
        }   
    }
}
