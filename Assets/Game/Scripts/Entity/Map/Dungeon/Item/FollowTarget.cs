using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FollowTarget : MonoBehaviour
{
    [SerializeField]protected Transform target;
    protected bool isFollow = false;
    protected IMoveBase moveModule;
    protected int speed = 9;
    Vector2 direction;
    // Update is called once per frame
    protected virtual void Start(){
        target = PlayerBase.player.transform;
        moveModule = GetComponent<IMoveBase>();
        direction = HelperClass.getRandomVector2D();
        Invoke("setFollow",0.3f);
    }
    protected virtual void FixedUpdate()
    {
        if(isFollow){
            if(((Vector2)target.position - (Vector2)transform.position).magnitude < 8f){
                followTarget();
            }
        }
        else{
            moveModule.SetVelocity(direction * speed * Time.fixedDeltaTime * 60 * 1.5f);
        }
    }
    protected void followTarget(){
        direction = ((Vector2)(target.transform.position - transform.position)).normalized;
        moveModule.SetVelocity(direction * speed * Time.fixedDeltaTime * 60);
    }
    protected void setFollow(){
        this.isFollow = true;
        direction = Vector2.zero;
    }
}
