using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : LongRangeWeapon
{
    
    
    public GameObject bullet;
    private Timer timer;
    private Transform firePoint;
    
    
    protected override void Awake(){ 
        firePoint = transform.GetChild(0).GetChild(0).transform;
        base.Awake();
    }

    protected override void Start(){
        base.Start();
        setLayer();
        timer = gameObject.AddComponent<Timer>();
    }
    public override void attack(Vector2 target){
        if(!timer.TimerIsStart){
            mechanism(target);
            timer.timeStart(reloadTime);
        }   
    }

    protected override void recoil()
    {
        float value = Random.Range(-(float)LongRangeWeapon.valueRecoil/accurancy,(float)LongRangeWeapon.valueRecoil/accurancy);
        Vector3 rotation = transform.localRotation.eulerAngles;
        rotation.z = rotation.z + value;
        transform.localRotation = Quaternion.Euler(rotation);

        float recoilTime = animatorModule.playAnimation(FIRE);
        isLookAt = false;
        Invoke("activeLookAt",recoilTime);
    }
    

    protected virtual void fireBullet(){
        recoil();
        Vector2 firePointPos = firePoint.transform.position;
        Vector2 fireDirection = gameObject.transform.right;
        GameObject instBullet = Instantiate(bullet,firePointPos,HelperClass.getQuaternion2Vector(Vector2.right,fireDirection));
        instBullet.GetComponent<Bullet>().setLayer(LayerMask.NameToLayer("Bullet"));
        instBullet.GetComponent<Bullet>().fire();
    }

    protected override void mechanism(Vector2 target){
        fireBullet();
    }
    
    public override void setLayer(){
        GameObject parent = null;
        if(transform.parent != null && transform.parent.gameObject.layer != LayerMask.NameToLayer("Environment") ){
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
