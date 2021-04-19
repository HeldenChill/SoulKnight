using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type1Bullet : Bullet
{

    public GameObject impactEffect;
    void Awake(){
    }
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

    public override void setLayer(int layer){
        this.gameObject.layer = layer;
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


}
