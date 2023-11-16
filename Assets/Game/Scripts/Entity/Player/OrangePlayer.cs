using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilitys;
using Utilitys.Timer;

public class OrangePlayer : PlayerBase,ICharacterBase
{
    float loadSkillTime = 1f;
    float skillTime = 0.1f;
    bool isSkill = false;
    STimer canUseSkill;
    protected override void Awake(){
        base.Awake();
        player = gameObject;
    }
    protected virtual void Start()
    {
        //Debug.Log(PlayerBase.player.name);
        canUseSkill = TimerManager.Inst.PopSTimer();
        gameObject.layer = LayerMask.NameToLayer("Player");
        layerMask = LayerMask.GetMask("Player");
        Weapon = GameHelper.FindWeapon(gameObject);
    }

    void FixedUpdate(){
        if(!isSkill){
            moveModule.SetVelocity(inputModule.MoveKeyBoard * speed);
        } 
    }

    protected override void Update()
    {
        if(Time.timeScale == 0) return; //Every line of code below this line will not call when timeScale equals 0
        base.Update();
        if(Input.GetMouseButton(0)){
            int value = weaponScripts.Attack(HelperClass.getMouse2DPosition());   
            if(value != 0){
                Mana -= value;
            }
        } 

        if(inputModule.EquipItem && contactItemModule.ItemInRange[0] != null){
            //Debug.Log(contactItemModule.ItemInRange[0].gameObject.name);
            contactItemModule.ItemInRange[0].gameObject.GetComponent<IItem>().GetItem();
        }

        if(inputModule.UseSkill){
            if(!isSkill){
                Skill();
            }
        }
        lookAtModule.LookAt(HelperClass.getMouse2DPosition());
        weaponScripts.Aim(HelperClass.getMouse2DPosition()); // Weapon will point to mouse position
    }

    public void ChangeWeapon(GameObject weapon){
        Weapon = weapon;
    }
    
    public int GetLayer(){
        return gameObject.layer;
    }
    public int GetLayerMask(){
        return LayerMask.GetMask(LayerMask.LayerToName(GetLayer()));
    }

    protected override void Skill(){
        if(!canUseSkill.IsStart){
            canUseSkill.Start(loadSkillTime);
            DashSkill();
        }
    }
    protected void DashSkill(){
        isSkill = true;
        Vector2 directionSkill = inputModule.MoveKeyBoard;
        moveModule.SetVelocity(directionSkill * speed * 5);
        TimerManager.Inst.WaitForTime(skillTime, EndSkill);
    }
    protected override void EndSkill()
    {
        isSkill = false;
    }
    protected override void Die()
    {
        Debug.Log(name + " die");
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer == LayerMask.NameToLayer("Bullet")){
            GetDamage(other.GetComponent<Bullet>().Damage);
        } 
        else if(other.gameObject.layer == LayerMask.NameToLayer("AutoAddItem")){
            other.GetComponent<IItem>().GetItem();
        }
    }
    

}
