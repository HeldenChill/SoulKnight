using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : Bullet
{
    void Awake(){ // Awake is suitable for setting initial values ​​for variables
        speed = 15;
        hp  = 5;
        rb = gameObject.GetComponent<Rigidbody2D>();
        Debug.Log("Awake");
    }
    public override void property()
    {
        
    }

    protected override void destroy()
    {
        
    }

    public override void Fire()
    {
        velocity = Vector2.right * speed;
        move();
    }

    public override void SetLayer(int layer)
    {
        throw new System.NotImplementedException();
    }

    protected override Vector2 move()
    {
        rb.velocity = velocity;
        return velocity;
    }
    
}
