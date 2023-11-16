using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : RigidbodyObject
{    
    protected abstract void Patrol();


    protected abstract void Combat();
}   
