using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;
    public float reloadTime = 1f;
    Timer timer;
    void Start(){
        timer = gameObject.AddComponent<Timer>();
    }
    public void attack(){
        if(!timer.TimerIsStart){
            mechanism();
            timer.timeStart(reloadTime);
        }   
    }

    protected void mechanism(){
        Vector2 firePointPos = firePoint.transform.position;
        Vector2 fireDirection = (HelperClass.getMouse2DPosition() - (Vector2)(gameObject.transform.position)).normalized;
        GameObject instBullet = Instantiate(bullet,firePointPos,HelperClass.getQuaternion2Vector(Vector2.right,fireDirection));
        instBullet.GetComponent<Bullet>().fire();
    }

    protected void playAnimation(){

    }

}
