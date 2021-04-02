using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsGameObject : MonoBehaviour
{
    protected int hp;
    protected int speed;
    protected int jumpForce;
    protected Vector2 velocity = Vector2.zero;
    protected Quaternion currentDirection;
    protected Rigidbody2D rb;
    protected bool isSkilling = false;
    protected Collider2D colCheckJump;
    protected LayerMask groundLayer;
    protected virtual void Start()
    {

    }
    protected virtual void Update()
    {
        move();
        jump();
    }
    protected virtual void FixedUpdate(){
        rb.velocity = velocity;
    }
    public virtual void Init(int hp,int speed,int jumpForce){
        Hp = hp;
        Speed = speed;
        JumpForce = jumpForce;
        currentDirection = transform.rotation;
        groundLayer = LayerMask.GetMask("Ground");
    }

    public int Hp{
        get{
            return hp;
        }
        set{
            hp = value;
        }
    }

    public int Speed{
        get{
            return speed;
        }
        set{
            speed = value;
        }
    }

    public int JumpForce{
        get{
            return JumpForce;
        }
        set{
            jumpForce = value;
        }
    }
    protected void flip(){
        if (transform.rotation.y == 0f)
        {
            currentDirection.y = 1f;
        }
        else if (Mathf.Abs(transform.rotation.y) == 1f)//????
        {
            currentDirection.y = 0f;
        }
        transform.rotation = currentDirection;
    }

    protected virtual void jump(){  
        rb.AddForce(new Vector2(0,jumpForce));
    }    
    protected virtual void move(){

    }
    protected virtual void getDamage(int damage){
        Hp -= damage;
    }

    protected virtual void playAnimation(){
        return;
    }
    protected virtual void attack(){

    }
}
