using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactItemModule : MonoBehaviour
{
    [SerializeField]private Vector2 sizeOfArea = new Vector2(0.1f,0.1f);
    [SerializeField]private GameObject buttonGUI;
    [SerializeField]private GameObject cursorGUI;
    private BoxCollider2D area;
    private Collider2D[] itemInRange = new Collider2D[5];
    private ContactFilter2D filter2D = new ContactFilter2D();
    void Start(){
        buttonGUI = Instantiate(buttonGUI,transform.position + new Vector3(0,1.3f,0),Quaternion.identity,gameObject.transform.parent);
        buttonGUI.GetComponent<ContactGUI>().Target = gameObject;
        buttonGUI.SetActive(false);

        cursorGUI = Instantiate(cursorGUI,transform.position,Quaternion.identity,gameObject.transform.parent);
        cursorGUI.SetActive(false);

        area = gameObject.AddComponent<BoxCollider2D>();
        area.isTrigger = true;
        filter2D.useTriggers = true;
        filter2D.SetLayerMask(LayerMask.GetMask("Item"));
        area.size = sizeOfArea;
    }
    void FixedUpdate(){
        contactItem();
    }
    void contactItem(){
        HelperClass.initArrayWithValue(itemInRange,null);
        area.OverlapCollider(filter2D,itemInRange);
        if(itemInRange[0] != null){
            buttonGUI.SetActive(true);

            cursorGUI.transform.position = itemInRange[0].gameObject.transform.position + new Vector3(0,1,0);
            cursorGUI.SetActive(true);
        }
        else{
            buttonGUI.SetActive(false);
            cursorGUI.SetActive(false);
        }
    }

    public Collider2D[] ItemInRange{
        get{
            return itemInRange;
        }
    }

}
