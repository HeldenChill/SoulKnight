using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTransformModule : MonoBehaviour,IMoveBase
{
    public void SetVelocity(Vector2 vel){
        gameObject.transform.position += (Vector3)vel * Time.fixedDeltaTime;
    }
}
