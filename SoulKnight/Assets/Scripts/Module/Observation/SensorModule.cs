using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction{
    right = 0,
    up = 1,
    left = 2,
    down  = 3
}
public class SensorModule : MonoBehaviour
{
    // Start is called before the first frame update
    RaycastHit2D[] sensor;
    private float radius = 0;

    private LayerMask detectingLayer;
    public float Radius{
        get{return radius;}
    }
    void Awake(){
        sensor = new RaycastHit2D[4];
    }
    void FixedUpdate()
    {
        if(radius > 0){
            observation();
        }    
    }

    // Update is called once per frame
    public void observation(){
        sensor[0] = Physics2D.Raycast(transform.position,Vector2.right,radius,detectingLayer);
        sensor[1] = Physics2D.Raycast(transform.position,Vector2.up,radius,detectingLayer);
        sensor[2] = Physics2D.Raycast(transform.position,Vector2.left,radius,detectingLayer);
        sensor[3] = Physics2D.Raycast(transform.position,Vector2.down,radius,detectingLayer); 
    }
    public void setUpObservation(float radius,LayerMask detectingLayer){
        if(radius >= 0)
            this.radius = radius;
        else
            this.radius = 0;

        this.detectingLayer = detectingLayer;
    }

    public RaycastHit2D[] getInfo(){
        if(radius > 0)
            return sensor;
        else
            return null;
    }
}
