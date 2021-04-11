using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : Bullet
{
    void Awake(){ // Awake is suitable for setting initial values ​​for variables
        speed = 15f;
        hp  = 5f;
        rb = gameObject.GetComponent<Rigidbody2D>();
        Debug.Log("Awake");
    }
    public override void property()
    {
        
    }

    protected override void destroy()
    {
        
    }

    public override void fire()
    {
        velocity = Vector2.right * speed;
        move();
    }

    public override void setLayer(int layer)
    {
        throw new System.NotImplementedException();
    }

    protected override Vector2 move()
    {
        rb.velocity = velocity;
        return velocity;
    }
    

    protected override void getDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

    protected override void playAnimation()
    {
        throw new System.NotImplementedException();
    }
    protected override void attack()
    {
        throw new System.NotImplementedException();
    }
}
