using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilitys.Timer;

public class Gun : LongRangeWeapon
{
    
    
    public GameObject bullet;
    private STimer timer;
    private Transform firePoint;
    
    protected override void Awake(){ 
        firePoint = transform.GetChild(0).GetChild(0).transform;
        manaToUse = 2;
        base.Awake();
        timer = TimerManager.Inst.PopSTimer();
    }

    protected override void Start(){
        base.Start();
        SetLayer();
    }
    public override int Attack(Vector2 target){
        if(!timer.IsStart){
            Mechanism(target);
            timer.Start(reloadTime);
            return manaToUse;
        }   
        return 0;
    }

    protected override void Recoil()
    {
        float value = Random.Range( -(float)valueRecoil/accurancy, (float)valueRecoil/accurancy);
        Vector3 rotation = transform.localRotation.eulerAngles;
        rotation.z = rotation.z + value;
        transform.localRotation = Quaternion.Euler(rotation);

        float recoilTime = animatorModule.playAnimation(FIRE);
        isLookAt = false;
        TimerManager.Inst.WaitForTime(recoilTime, ActiveLookAt);

        void ActiveLookAt()
        {
            isLookAt = true;
        }
    }
    

    protected virtual void FireBullet(){
        Recoil();
        Vector2 firePointPos = firePoint.transform.position;
        Vector2 fireDirection = gameObject.transform.right;
        GameObject instBullet = Instantiate(bullet,firePointPos,HelperClass.getQuaternion2Vector(Vector2.right,fireDirection));
        instBullet.GetComponent<Bullet>().SetLayer(LayerMask.NameToLayer("Bullet"));
        instBullet.GetComponent<Bullet>().Fire();
    }

    protected override void Mechanism(Vector2 target){
        FireBullet();
    }
    
    public override void SetLayer(){
        GameObject parent = null;
        if(transform.parent != null && transform.parent.gameObject.layer != LayerMask.NameToLayer("Environment") ){
            parent = transform.parent.gameObject;
        }
        if(parent != null){
            int parentLayer = parent.GetComponent<ICharacterBase>().GetLayer();
            if(parentLayer == LayerMask.NameToLayer("Player")){
                gameObject.layer = LayerMask.NameToLayer("Item");
            }
            else if(parentLayer == LayerMask.NameToLayer("Enemy")){
                gameObject.layer = LayerMask.NameToLayer("Item");
            }
        }
        
    }
}
