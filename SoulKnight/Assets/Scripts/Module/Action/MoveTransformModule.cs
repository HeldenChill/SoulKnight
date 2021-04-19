using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTransformModule : MonoBehaviour,IMoveBase
{
    public void setVelocity(Vector2 vel){
        gameObject.transform.position += (Vector3)vel * Time.fixedDeltaTime;
    }
}
