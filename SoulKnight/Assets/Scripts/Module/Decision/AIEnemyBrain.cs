using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemyBrain : MonoBehaviour
{
    //atribute
    public float speed = 1;
    private Vector2 direction;
    private Transform target;
    private bool isStopNextStep = true; //for patrol
    private Timer timer;
    private GameObject weapon;
    
    //module
    private MoveVelocityRBModule moveModule;
    private SensorModule sensor;
    private FilpForDirectionView filpModule;
    
    void Start()
    {
        timer = gameObject.AddComponent<Timer>();

        weapon = GameMechanism.findWeapon(this.gameObject);
        filpModule = GetComponent<FilpForDirectionView>();
        moveModule = GetComponent<MoveVelocityRBModule>();
        sensor = GetComponent<SensorModule>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        target = OrangePlayer.player.transform;
        filpModule.lookAt(target.position);
        decision();
        moveModule.setVelocity(direction * speed);    
    }
    public void decision(){
        patrol(1.5f);
        weapon.GetComponent<Weapon>().attack(target.position);
    }
    
    public void patrol(float stepTime){
        float minAngle = 0f;
        float maxAngle = 360f;
        
            //detecting wall
        if(detecting(1,LayerMask.GetMask("Wall"))){
            
            RaycastHit2D[] hits = sensor.getInfo();
            if(hits[0].collider != null ){ 
                minAngle = 90f;
                maxAngle = 270f;
            }
            if(hits[1].collider != null){
                minAngle = 180f;
            }
            if(hits[2].collider != null){
                minAngle = -90f;
                maxAngle = 90f;
            }
            if(hits[3].collider != null){
                maxAngle = 180f;
            }
            setRandomDirectionMove(minAngle,maxAngle);
            return;
            
        }

            //don't detecting wall
        if(!timer.TimerIsStart){  
            timer.timeStart(stepTime);
            isStopNextStep = !isStopNextStep; 
            if(isStopNextStep){
                direction = Vector2.zero;
                return;
            }
            else{
                setRandomDirectionMove(minAngle,maxAngle);
                return;
            }      
            
        }
    }
    
    

    private void setRandomDirectionMove(float minAngle,float maxAngle){
        direction = HelperClass.getRandomDirection(minAngle,maxAngle);
    }
    private bool detecting(float range,LayerMask layer){
        sensor.setUpObservation(range,layer);
        foreach(var hit in sensor.getInfo()){
            if(hit.collider != null){
                return true;
            }
        }
        return false;
    }
        
}
