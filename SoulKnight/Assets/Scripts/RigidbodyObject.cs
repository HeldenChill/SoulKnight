using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RigidbodyObject : MoveObject
{
    protected Rigidbody2D rb;
    protected LayerMask wallLayer;

 
    public override void Init(float hp,float speed){ //Function we use to initialize the variable of Object(to reduce the use of Constructor)
        this.hp = hp;
        this.speed = speed;
        this.currentRotation = transform.rotation;
        wallLayer = LayerMask.GetMask("Wall");
        
    }

    protected abstract void getDamage(int damage);

    protected abstract void playAnimation();
    protected abstract void attack();

    
}
