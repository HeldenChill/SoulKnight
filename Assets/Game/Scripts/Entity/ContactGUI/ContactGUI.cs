using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactGUI : MonoBehaviour
{
    private GameObject target;
    private Vector3 localPosToTarget;
    public GameObject Target{
        get{return target;}
        set{
            target = value;
            localPosToTarget = transform.position - target.transform.position;
        }
    }
    void Update()
    {
        if(target != null){
            followTarget();
        }
        
    }

    public void followTarget(){
        transform.position = target.transform.position + localPosToTarget;
    }
}
