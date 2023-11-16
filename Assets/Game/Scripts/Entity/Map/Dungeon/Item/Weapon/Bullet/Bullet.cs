using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected int speed = 20;
    [SerializeField] protected int hp = 1;
    [SerializeField] protected int damage = 2;

    public int Damage{
        get{return damage;}
    }
    public int Speed{
        get{return speed;}
    }
    protected Rigidbody2D rb;
    protected Vector2 velocity;
    public virtual void Start()
    {

    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        
    }
    public abstract void SetLayer(int layer);
    public abstract void property();
    public abstract void Fire();

    protected abstract void destroy();
    protected abstract Vector2 move();
}
