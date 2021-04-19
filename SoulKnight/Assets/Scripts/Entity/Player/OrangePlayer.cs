using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangePlayer : PlayerBase,ICharacterBase
{
    float loadSkillTime = 1f;
    float skillTime = 0.1f;
    bool isSkill = false;
    Timer canUseSkill;
    protected override void Awake(){
        base.Awake();
        PlayerBase.player = gameObject;
    }
    protected virtual void Start()
    {
        //Debug.Log(PlayerBase.player.name);
        canUseSkill = gameObject.AddComponent<Timer>();
        gameObject.layer = LayerMask.NameToLayer("Player");
        layerMask = LayerMask.GetMask("Player");
        weapon = GameHelper.findWeapon(this.gameObject);
    }

    void FixedUpdate(){
        if(!isSkill){
            moveModule.setVelocity(inputModule.MoveKeyBoard * speed);
        } 
    }

    protected virtual void Update()
    {

        if(Input.GetMouseButton(0)){
            int value = weapon.GetComponent<Weapon>().attack(HelperClass.getMouse2DPosition());   
            if(value != 0){
                Mana -= value;
            }
        } 

        if(inputModule.EquipItem && contactItemModule.ItemInRange[0] != null){
            //Debug.Log(contactItemModule.ItemInRange[0].gameObject.name);
            contactItemModule.ItemInRange[0].gameObject.GetComponent<IItem>().getItem();
        }

        if(inputModule.UseSkill){
            if(!isSkill){
                skill();
            }
        }
        lookAtModule.lookAt(HelperClass.getMouse2DPosition());
        weapon.GetComponent<Weapon>().aim(HelperClass.getMouse2DPosition());
    }

    public void changeWeapon(GameObject weapon){
        Weapon = weapon;
    }
    
    public int getLayer(){
        return gameObject.layer;
    }
    public int getLayerMask(){
        return LayerMask.GetMask(LayerMask.LayerToName(getLayer()));
    }

    protected override void skill(){
        if(!canUseSkill.TimerIsStart){
            canUseSkill.timeStart(loadSkillTime);
            dashSkill();
        }
    }
    protected void dashSkill(){
        isSkill = true;
        Vector2 directionSkill = inputModule.MoveKeyBoard;
        moveModule.setVelocity(directionSkill * speed * 5);
        Invoke("endSkill",skillTime);
    }
    protected override void endSkill()
    {
        isSkill = false;
    }
    protected override void die()
    {
        Debug.Log(name + " die");
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer == LayerMask.NameToLayer("Bullet")){
            getDamage(other.GetComponent<Bullet>().Damage);
        } 
        else if(other.gameObject.layer == LayerMask.NameToLayer("AutoAddItem")){
            other.GetComponent<IItem>().getItem();
        }
    }
    

}
