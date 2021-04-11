using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactPlayerModule : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private Vector2 sizeOfArea = new Vector2(2f,2f);
    private bool active = true;
    private BoxCollider2D area;
    private Collider2D[] playerInRange = new Collider2D[5];
    private ContactFilter2D filter2D = new ContactFilter2D();

    public bool Active{
        get{return active;}
        set{
            if(value == true){
                this.enabled = true;
                area.enabled = true;
            }
            else{
                area.enabled = false;
                this.enabled = false;
            }
        }
    }
    void Awake(){
        area = gameObject.AddComponent<BoxCollider2D>();
        area.isTrigger = true;
        filter2D.useTriggers = true;
        filter2D.SetLayerMask(LayerMask.GetMask("Player"));
        area.size = sizeOfArea;
    }
    void contactPlayer(){
        HelperClass.initArrayWithValue(playerInRange,null);
        area.OverlapCollider(filter2D,playerInRange);
    }


    public Collider2D[] PlayerInRange{
        get{
            contactPlayer();
            return playerInRange;
        }
    }

    public BoxCollider2D Area{
        get{return area;}
        set{area = value;}
    }
}
