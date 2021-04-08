using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemyBrain : MonoBehaviour,ICharacterBase
{
    //atribute
    public int roomInID = -1;
    public float speed = 1;
    public int hp = 20;
    private float timePatrol;
    private Vector2 direction;
    private Transform target;
    private bool isStopNextStep = true; //for patrol
    private Timer timer;
    private Timer timeAttack;
    private GameObject weapon;
    
    //module
    private MoveVelocityRBModule moveModule;
    private SensorModule sensor;
    private FilpForDirectionView filpModule;
    
    //setup enemy
    void Awake()
    {
        timer = gameObject.AddComponent<Timer>();
        EventManager.current.onPlayerEnterMonsterRoom += trigger;
        this.enabled = false;
    }
    void OnDestroy(){
        EventManager.current.onPlayerEnterMonsterRoom -= trigger;
    }
    void Start(){
        weapon = GameHelper.findWeapon(this.gameObject);
        filpModule = GetComponent<FilpForDirectionView>();
        moveModule = GetComponent<MoveVelocityRBModule>();
        sensor = GetComponent<SensorModule>();
    }
    //enemy active
    void FixedUpdate()
    {
        target = OrangePlayer.player.transform;
        filpModule.lookAt(target.position);
        decide();
        moveModule.setVelocity(direction * speed);   
         
    }
    //trigger enemy
    private void trigger(int id){
        if(id == this.roomInID){
            //this.isTrigger = true;
            this.enabled = true;
        }
    }

    //enemy hehaviour
        //decision
    public void decide(){
        timePatrol = Random.Range(1f,2f);
        patrol(timePatrol);
        weapon.GetComponent<Weapon>().attack(target.position);  
    }
        //patrol
    public void patrol(float stepTime){
        float minAngle = 0f;
        float maxAngle = 360f;
        
            //detecting wall
        if(detecting(1,LayerMask.GetMask("Wall"))){
            RaycastHit2D[] hits = sensor.getInfo();
            if(hits[0].collider != null ){ 
                minAngle = minAngle > 90f ? minAngle : 90f;
                maxAngle = maxAngle < 270f ? maxAngle : 270f;
            }
            if(hits[1].collider != null){
                minAngle = minAngle > 180 ? minAngle : 180;
                maxAngle = maxAngle < 360f ? maxAngle : 360f;
            }
            if(hits[2].collider != null){
                minAngle = minAngle > 270f ? minAngle : 270f;
                maxAngle = maxAngle < 450f ? maxAngle : 450f;
            }
            if(hits[3].collider != null){
                minAngle = minAngle > 0f ? minAngle : 0f;
                maxAngle = maxAngle < 180f ? maxAngle : 180f;
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
        //get damage
    public void getDamage(int damage){
        hp -= damage;
        if(hp < 0){
            die();
        }
    }

    public void getMana(int mana){

    }
    public void changeWeapon(GameObject weapon){
        
    }
        //die
    private void die(){
        EventManager.current.EnemyDie(roomInID);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer == LayerMask.NameToLayer("Bullet")){
            getDamage(5);
        }    
    }
    //get layer and mask
    public int getLayer(){
        return gameObject.layer;
    }

    public int getLayerMask(){
        return LayerMask.GetMask(LayerMask.LayerToName(getLayer()));
    }
    //helper function
    private void setRandomDirectionMove(float minAngle,float maxAngle){
        direction = HelperClass.getRandomDirection(minAngle,maxAngle);
    }
    private bool detecting(float range,LayerMask layer){
        sensor.setUpObservation(range,layer);

        if(sensor.getInfo() == null) return false;

        foreach(var hit in sensor.getInfo()){
            if(hit.collider != null){
                return true;
            }
        }
        return false;
    }

    //when enemy is destroy
    
        
}
