using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    // Start is called before the first frame update
    public int roomInID = -1;
    public float speed = 1;
    public int hp = 20;
    protected Vector2 direction;
    protected Transform target;
    protected MoveVelocityRBModule moveModule;
    protected SensorModule sensor;
    protected LookAtModule lookAtModule;
    protected GameObject weapon;

    public int Hp{
        get{return hp;}
        set{
            if(hp <= 0){
                die();
                hp = 0;
            }
            else{
                hp = value;
            }
        }
    }
    protected virtual void Awake()
    {
        if(TryGetComponent<LookAtModule>(out LookAtModule _lookAtModule)){
            lookAtModule = _lookAtModule;
        }
        else{
            Debug.LogError(name + " dont have " + typeof(LookAtModule).Name);
        }

        if(TryGetComponent<MoveVelocityRBModule>(out MoveVelocityRBModule _moveModule)){
            moveModule = _moveModule;
        }
        else{
            Debug.LogError(name + " dont have " + typeof(MoveVelocityRBModule).Name);
        }

        if(TryGetComponent<SensorModule>(out SensorModule _sensor)){
            sensor = _sensor;
        }
        else{
            Debug.LogError(name + " dont have " + typeof(SensorModule).Name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected void trigger(int id){
        if(id == this.roomInID){
            //this.isTrigger = true;
            this.enabled = true;
        }
    }
    protected virtual void die(){

    }
    protected void setRandomDirectionMove(float minAngle,float maxAngle){
        direction = HelperClass.getRandomDirection(minAngle,maxAngle);
    }

    public virtual void getDamage(int damage){
        Hp -= damage;
    }
}
