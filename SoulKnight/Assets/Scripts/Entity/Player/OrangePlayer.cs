using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangePlayer : PlayerBase,ICharacterBase
{
     
    protected override void Awake(){
        base.Awake();
        PlayerBase.player = gameObject;
    }
    protected virtual void Start()
    {
        //Debug.Log(PlayerBase.player.name);
        gameObject.layer = LayerMask.NameToLayer("Player");
        layerMask = LayerMask.GetMask("Player");
        weapon = GameHelper.findWeapon(this.gameObject);
    }

    void FixedUpdate(){
        moveModule.setVelocity(inputModule.MoveKeyBoard * speed);
    }

    protected virtual void Update()
    {
        
        if(Input.GetMouseButton(0)){
            weapon.GetComponent<Weapon>().attack(HelperClass.getMouse2DPosition());   
        } 
        if(inputModule.EquipItem && contactItemModule.ItemInRange[0] != null){
            //Debug.Log(contactItemModule.ItemInRange[0].gameObject.name);
            contactItemModule.ItemInRange[0].gameObject.GetComponent<IItem>().getItem();
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

}
