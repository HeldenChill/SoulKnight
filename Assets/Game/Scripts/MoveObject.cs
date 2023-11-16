using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveObject : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float hp = 0;
    public float speed = 0;
    protected Vector2 velocity;
    protected Vector2 moveDirection = Vector2.zero;
    protected Quaternion currentRotation;
    public virtual void Init(float hp,float speed){
        this.hp = hp;
        this.speed = speed;
    }

    protected abstract Vector2 Move();
    protected abstract void OnDespawn();

    
}
