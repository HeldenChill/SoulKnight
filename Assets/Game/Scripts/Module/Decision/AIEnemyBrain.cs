using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilitys.Timer;

public class AIEnemyBrain : EnemyBase,ICharacterBase
{
    //atribute
    private float timePatrol;
    private bool isStopNextStep = true; //for patrol
    private STimer timer;
    private STimer timeAttack;
    //setup enemy
    protected override void Awake()
    {
        base.Awake();
        timer = TimerManager.Inst.PopSTimer();
        timeAttack = TimerManager.Inst.PopSTimer();
        EventManager.Inst.onPlayerEnterMonsterRoom += trigger;
        this.enabled = false;
    }
    void OnDestroy(){
        EventManager.Inst.onPlayerEnterMonsterRoom -= trigger;
    }
    void Start(){
        Weapon = GameHelper.FindWeapon(this.gameObject);
    }
    //enemy active
    void FixedUpdate()
    {
        target = PlayerBase.player.transform;
        lookAtModule.LookAt(target.position);
        weaponScripts.Aim(target.position);
        Decide();
        moveModule.SetVelocity(direction * speed);   
         
    }
    //trigger enemy
    

    //enemy hehaviour
        //decision
    public void Decide(){
        timePatrol = Random.Range(1f,2f);
        Patrol(timePatrol);
        weaponScripts.Attack(target.position);  
    }
        //patrol
    public void Patrol(float stepTime){
        float minAngle = 0f;
        float maxAngle = 360f;
        
            //detecting wall
        if(Detecting(1,LayerMask.GetMask("Wall"))){
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
            SetRandomDirectionMove(minAngle,maxAngle);
            return;   
        }

            //don't detecting wall
        if(!timer.IsStart){  
            timer.Start(stepTime);
            isStopNextStep = !isStopNextStep; 
            if(isStopNextStep){
                direction = Vector2.zero;
                return;
            }
            else{
                SetRandomDirectionMove(minAngle,maxAngle);
                return;
            }      
            
        }
    }
        //get damage
    public override void GetDamage(int damage){
        Hp -= damage;
    }
    public void ChangeWeapon(GameObject weapon){
        
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer == LayerMask.NameToLayer("Bullet")){
            GetDamage(other.GetComponent<Bullet>().Damage);
        }    
    }
        //die
    protected override void Die(){
        EventManager.Inst.createNormalReward(transform.position,1);
        EventManager.Inst.EnemyDie(RoomInID);
        Destroy(gameObject);
    }

    
    //get layer and mask
    public int GetLayer(){
        return gameObject.layer;
    }

    public int GetLayerMask(){
        return LayerMask.GetMask(LayerMask.LayerToName(GetLayer()));
    }
    //helper function
    
    private bool Detecting(float range,LayerMask layer){
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
