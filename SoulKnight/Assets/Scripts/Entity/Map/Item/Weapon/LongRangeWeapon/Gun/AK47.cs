using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47 : Gun
{
    private PuzzleFlare puzzleFlare;
    private float timeBetweenBullet = 0.05f;
    private int numberOfBullet = 2;
    protected override void Awake(){
        base.Awake();
        puzzleFlare = transform.GetChild(1).GetComponent<PuzzleFlare>();
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
            puzzleFlare.activeFlare();
            yield return new WaitForSeconds(timeBetweenBullet);
        }
        StopCoroutine(fireMultipleBullet());
    }
}
