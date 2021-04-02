using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type1Bullet : Bullet
{

    public GameObject impactEffect;
    public override void Start()
    {
        base.Start();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        velocity = gameObject.transform.right * speed;
    }
    // Update is called once per frame
    public override void FixedUpdate()
    {
        move();
    }

    public override void property()
    {
        
    }

    public override void fire()
    {
    
    }

    protected override Vector2 move()
    {
        rb.velocity = velocity;
        return velocity;
    }

    protected override void destroy(){
        Instantiate(impactEffect,transform.position,transform.rotation);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other){
        destroy();
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
