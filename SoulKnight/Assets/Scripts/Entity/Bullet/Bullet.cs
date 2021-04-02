using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : RigidbodyObject
{

    public virtual void Start()
    {

    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        move();
    }
    public abstract void property();
    public abstract void fire();
}
