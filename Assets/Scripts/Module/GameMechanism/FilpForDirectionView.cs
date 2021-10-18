using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilpForDirectionView : MonoBehaviour
{
    private Vector3 initPlayerScale;
    private Vector3 initWeaponScale;
    private GameObject weapon;

    public GameObject Weapon{
        get{return weapon;}
        set
        {
            weapon = value;
            initWeaponScale = weapon.transform.localScale;
            initWeaponScale = new Vector3(Mathf.Abs(initWeaponScale.x),Mathf.Abs(initWeaponScale.y),initWeaponScale.z);
        }
    }
    void Awake(){
        initPlayerScale = transform.localScale;
        Weapon = GameHelper.findWeapon(this.gameObject);
    }

    public void lookAt(Vector2 face,Vector2 point){
        Vector2 thisPosition = new Vector2(transform.position.x,transform.position.y); //the position of player(Convert from Vector3->Vector2)
        Vector2 vectorToPoint = point - thisPosition;   //vectorToPoint through 2 point,the position of player and the position of mouse(HelperClass.getMouse2DPosition())
        if(point.x < thisPosition.x){
            this.gameObject.transform.localScale = new Vector3(initPlayerScale.x * -1,initPlayerScale.y,initPlayerScale.z);
            if(weapon != null)
                weapon.transform.localScale = new Vector3(initWeaponScale.x * -1,initWeaponScale.y * -1,initWeaponScale.z);
        }
        else{
            this.gameObject.transform.localScale = initPlayerScale;
            if(weapon != null)
                weapon.transform.localScale = initWeaponScale;
        }
        if(weapon != null)
            HelperClass.rotateObject(face,vectorToPoint,weapon);
    }

    public void lookAt(Vector2 point){
        lookAt(Vector2.right,point);
    }
}
