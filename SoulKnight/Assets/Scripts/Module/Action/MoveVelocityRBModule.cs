using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVelocityRBModule : MonoBehaviour,IMoveBase
{
    // Start is called before the first frame update
    private Vector2 velocity = Vector2.zero;
    private Rigidbody2D rb;
    void Start()
    {
        if(gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidbody)){
            rb = rigidbody;
        }
        else{
            Debug.Log("Error:Object don't have RigidBody2D");
        }
    }

    // Update is called once per frame

    public void setVelocity(Vector2 vel){
        velocity = vel;
        rb.velocity = velocity;
    }
}
