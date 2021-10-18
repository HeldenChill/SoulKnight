using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M4A1 : Gun
{
    // Start is called before the first frame update
    private float timeBetweenBullet = 0.05f;
    private int numberOfBullet = 3;
    protected override void Awake(){
        base.Awake();
        reloadTime = 0.6f;
        accurancy = 60;
    }
    protected override void mechanism(Vector2 target)
    {
        StartCoroutine(fireMultipleBullet());
    }

    private IEnumerator fireMultipleBullet(){
        for(int i = 0; i < numberOfBullet; i++){
            fireBullet();
            AudioManager.current.Play("M4A1_Fire");
            yield return new WaitForSeconds(timeBetweenBullet);
        }
        StopCoroutine(fireMultipleBullet());
    }
}
