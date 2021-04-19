using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtModule : MonoBehaviour
{   
    Vector3 initScale;
    bool isWeapon = false;
    public void Start(){
        Vector3 scale = transform.localScale;
        initScale = new Vector3(Mathf.Abs(scale.x),Mathf.Abs(scale.y),scale.z);
    }
    public bool IsWeapon{
        get{return isWeapon;}
        set{
            isWeapon = value;
        }
    }
    public void lookAt(Vector2 face,Vector2 point){
        Vector2 thisPosition = new Vector2(transform.position.x,transform.position.y); //the position of player(Convert from Vector3->Vector2)
        Vector2 vectorToPoint = point - thisPosition;   //vectorToPoint through 2 point,the position of player and the position of mouse(HelperClass.getMouse2DPosition())
        if(gameObject != null){
            if(point.x < thisPosition.x){
                if(isWeapon){
                    gameObject.transform.localScale = new Vector3(-initScale.x,-initScale.y,initScale.z);
                }
                else{
                    gameObject.transform.localScale = new Vector3(initScale.x * -1,initScale.y,initScale.z);
                }
                    
            }
            else{
                gameObject.transform.localScale = initScale;
            }
            
            if(isWeapon){
                HelperClass.rotateObject(face,vectorToPoint,gameObject);
            }
                
        }        
    }

    public void lookAt(Vector2 point){
        lookAt(Vector2.right,point);
    }
}
