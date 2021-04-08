using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactItemModule : MonoBehaviour
{
    [SerializeField]private Vector2 sizeOfArea = new Vector2(1f,1f);
    private BoxCollider2D area;
    private Collider2D[] itemInRange = new Collider2D[10];
    private ContactFilter2D filter2D = new ContactFilter2D();
    void Start(){
        area = gameObject.AddComponent<BoxCollider2D>();
        area.isTrigger = true;
        filter2D.useTriggers = true;
        filter2D.SetLayerMask(LayerMask.GetMask("Item"));
        area.size = sizeOfArea;
    }
    void FixedUpdate(){
        area.OverlapCollider(filter2D,itemInRange);
    }

    public Collider2D[] ItemInRange{
        get{return itemInRange;}
    }

}
