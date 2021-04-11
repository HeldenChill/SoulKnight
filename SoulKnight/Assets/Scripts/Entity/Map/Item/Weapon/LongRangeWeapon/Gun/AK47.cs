using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47 : Gun
{
    public ParticleSystem muzzleFlash;
    private float timeBetweenBullet = 0.05f;
    private int numberOfBullet = 2;
    protected override void Awake(){
        base.Awake();
        reloadTime = 0.3f;
        accurancy = 40;
    }
    protected override void mechanism(Vector2 target)
    {
        StartCoroutine(fireMultipleBullet());
    }

    private IEnumerator fireMultipleBullet(){
        for(int i = 0; i < numberOfBullet; i++){
            fireBullet();
            muzzleFlash.Emit(30);
            yield return new WaitForSeconds(timeBetweenBullet);
        }
        StopCoroutine(fireMultipleBullet());
    }
}
