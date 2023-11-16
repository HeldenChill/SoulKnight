using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVelocityRBModule : MonoBehaviour,IMoveBase
{

    private Vector2 velocity = Vector2.zero;
    [SerializeField]
    private Rigidbody2D rb;

    public void SetVelocity(Vector2 vel){
        velocity = vel;
        rb.velocity = velocity;
    }
}
