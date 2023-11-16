using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilitys.Timer;

public class NormalEnemy : Enemy
{
    STimer timer;

    protected virtual void Start()
    {
        speed = 5;
        timer = TimerManager.Inst.PopSTimer();
        Patrol();
    }
    protected override void Patrol(){
        MovingSetup();
        float time = Random.Range(3f, 5f);
        timer.Start(time, MovingSetup);

        void MovingSetup()
        {
            int x = Random.Range(-1, 2);
            int y = Random.Range(-1, 2);        
            moveDirection = new Vector2(x, y).normalized;
            Move();
        }                 
    }

    protected override Vector2 Move(){
        velocity = speed * moveDirection;
        rb.AddTorque(100);
        rb.velocity = velocity;
        return velocity;
    }

    protected override void Combat(){
        Debug.Log("Combat");
    }

    void OnCollisionExit2D(Collision2D col){
        timer.Stop();
        Patrol();
    }
    protected override void Attack()
    {

    }
    
    protected override void GetDamage(int damage){

    }

    protected override void PlayAnimation(){

    }

    protected override void OnDespawn()
    {
       
    }
}   
