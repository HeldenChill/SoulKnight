using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    public Transform firePoint;
    public GameObject bullet;
    public float reloadTime = 0.3f;

    private bool canShoot = true;
    Timer timer;
    void Start(){
        timer = gameObject.AddComponent<Timer>();
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
        instBullet.GetComponent<Bullet>().fire();
    }

    protected void playAnimation(){

    }

}
