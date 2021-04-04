using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    // Start is called before the first frame update
    public Collider2D[] tables;
    public bool isTrigger = true;
    void Start()
    {
        tables = gameObject.GetComponents<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isTrigger){
            if(tables[1].IsTouchingLayers(OrangePlayer.layer)){
                Debug.Log("Shop:Table1");
            }

            if(tables[2].IsTouchingLayers(OrangePlayer.layer)){
                Debug.Log("Shop:Table2");
            }

            if(tables[3].IsTouchingLayers(OrangePlayer.layer)){
                Debug.Log("Shop:Table3");
            }

            if(tables[4].IsTouchingLayers(OrangePlayer.layer)){
                Debug.Log("Shop:Table4");
            }
        }
    }
}
