using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : Enemy
{
    Timer timer;

    protected virtual void Awake(){
        timer = gameObject.AddComponent<Timer>();
    }
    protected virtual void Start()
    {
        timer.TimeOut += patrol;
        speed = 5;
        rb = gameObject.GetComponent<Rigidbody2D>();
        patrol();
    }
    protected override void patrol(){
        int x = Random.Range(-1,2);
        int y = Random.Range(-1,2);
        float time = Random.Range(3f,5f);

        moveDirection = new Vector2(x,y).normalized;
        move();
        timer.timeStart(time);
    }

    protected override Vector2 move(){
        velocity = speed * moveDirection;
        rb.AddTorque(100);
        rb.velocity = velocity;
        return velocity;
    }

    protected override void combat(){
        Debug.Log("Combat");
    }

    void OnCollisionExit2D(Collision2D col){
        timer.timeStop();
        patrol();
    }
    protected override void attack()
    {

    }
    
    protected override void getDamage(int damage){

    }

    protected override void playAnimation(){

    }

    protected override void destroy()
    {
       
    }
}   
