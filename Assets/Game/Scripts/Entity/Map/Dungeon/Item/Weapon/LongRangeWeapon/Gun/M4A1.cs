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
    }
    protected override void Mechanism(Vector2 target)
    {
        StartCoroutine(FireMultipleBullet());
    }

    private IEnumerator FireMultipleBullet(){
        for(int i = 0; i < numberOfBullet; i++){
            FireBullet();
            AudioManager.Inst.Play("M4A1_Fire");
            yield return new WaitForSeconds(timeBetweenBullet);
        }
        StopCoroutine(FireMultipleBullet());
    }
}
