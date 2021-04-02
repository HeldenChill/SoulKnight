using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform followedObject;
    public float dampTime = 0.4f;
    private Vector3 cameraPos;
    private Vector3 velocity = Vector3.zero;
    void FixedUpdate()
    {
        cameraPos = new Vector3(followedObject.position.x,followedObject.position.y,-10f);
        transform.position = Vector3.SmoothDamp(gameObject.transform.position,cameraPos,ref velocity,dampTime);
    }
}
