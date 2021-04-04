using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    // Start is called before the first frame update
    public Collider2D areaOfPortal;
    void Start()
    {
        areaOfPortal = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(areaOfPortal.IsTouchingLayers(OrangePlayer.layer)){
            Debug.Log("Portal:Teleport.");
        }
    }
}
