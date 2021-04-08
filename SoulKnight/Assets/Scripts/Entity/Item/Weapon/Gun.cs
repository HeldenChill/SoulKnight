using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    public Transform firePoint;
    public GameObject bullet;
    public float reloadTime = 0.3f;
    private int value = 18;
    private Timer timer;
    void Awake(){
        timer = gameObject.AddComponent<Timer>();
        contactPlayer = GetComponent<ContactPlayerModule>();
        changeWeapon = GetComponent<ChangeWeaponModule>();
    }

    void Start(){
        setLayer();
        
        if(transform.parent != null){
            contactPlayer.Area.enabled = false;
            contactPlayer.enabled = false;    
        }
    }
    public override void attack(Vector2 target){
        if(!timer.TimerIsStart){
            mechanism(target);
            timer.timeStart(reloadTime);
        }   
    }

    public override void mechanism(Vector2 target){
        Vector2 firePointPos = firePoint.transform.position;
        Vector2 fireDirection = gameObject.transform.right;
        GameObject instBullet = Instantiate(bullet,firePointPos,HelperClass.getQuaternion2Vector(Vector2.right,fireDirection));
        instBullet.GetComponent<Bullet>().setLayer(LayerMask.NameToLayer("Bullet"));
        instBullet.GetComponent<Bullet>().fire();
    }

    public override void getItem(){
        if(contactPlayer.PlayerInRange[0] != null){
            GameObject weaponOfPlayer = GameHelper.findWeapon(contactPlayer.PlayerInRange[0].gameObject);
            changeWeapon.changeWeapon(weaponOfPlayer);
        }
    }

    public override int getValue()
    {
        return value;
    }
    public override void setActiveContact(bool isActive)
    {
        contactPlayer.Active = isActive;
    }
    public override void setLayer(){
        GameObject parent = null;
        if(transform.parent != null){
            parent = transform.parent.gameObject;
        }
        if(parent != null){
            int parentLayer = parent.GetComponent<ICharacterBase>().getLayer();
            if(parentLayer == LayerMask.NameToLayer("Player")){
                gameObject.layer = LayerMask.NameToLayer("Item");
            }
            else if(parentLayer == LayerMask.NameToLayer("Enemy")){
                gameObject.layer = LayerMask.NameToLayer("Item");
            }
        }
        
    }
    protected void playAnimation(){

    }

}
