using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemyBrain : EnemyBase,ICharacterBase
{
    //atribute
    private float timePatrol;
    private bool isStopNextStep = true; //for patrol
    private Timer timer;
    private Timer timeAttack;
    //setup enemy
    protected override void Awake()
    {
        base.Awake();
        timer = gameObject.AddComponent<Timer>();
        EventManager.current.onPlayerEnterMonsterRoom += trigger;
        this.enabled = false;
    }
    void OnDestroy(){
        EventManager.current.onPlayerEnterMonsterRoom -= trigger;
    }
    void Start(){
        weapon = GameHelper.findWeapon(this.gameObject);
    }
    //enemy active
    void FixedUpdate()
    {
        target = PlayerBase.player.transform;
        lookAtModule.lookAt(target.position);
        weapon.GetComponent<LongRangeWeapon>().aim(target.position);
        decide();
        moveModule.setVelocity(direction * speed);   
         
    }
    //trigger enemy
    

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
    public override void getDamage(int damage){
        Hp -= damage;
    }
    public void changeWeapon(GameObject weapon){
        
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer == LayerMask.NameToLayer("Bullet")){
            getDamage(other.GetComponent<Bullet>().Damage);
        }    
    }
        //die
    protected override void die(){
        EventManager.current.createNormalReward(transform.position,1);
        EventManager.current.EnemyDie(roomInID);
        Destroy(gameObject);
    }

    
    //get layer and mask
    public int getLayer(){
        return gameObject.layer;
    }

    public int getLayerMask(){
        return LayerMask.GetMask(LayerMask.LayerToName(getLayer()));
    }
    //helper function
    
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
