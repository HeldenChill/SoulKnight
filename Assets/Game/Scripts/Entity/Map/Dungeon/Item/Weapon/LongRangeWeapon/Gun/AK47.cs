using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47 : Gun
{
    private PuzzleFlare puzzleFlare;
    [SerializeField] 
    private float timeBetweenBullet = 0.05f;
    [SerializeField]
    private int numberOfBullet = 2;
    protected override void Awake(){
        base.Awake();
        puzzleFlare = transform.GetChild(1).GetComponent<PuzzleFlare>();
        reloadTime = 0.3f;
        accurancy = 40;
    }
    protected override void Mechanism(Vector2 target)
    {
        StartCoroutine(FireMultipleBullet());
    }

    private IEnumerator FireMultipleBullet(){
        for(int i = 0; i < numberOfBullet; i++){
            FireBullet();
            AudioManager.Inst.Play("AK47_Fire");
            puzzleFlare.ActiveFlare();
            yield return new WaitForSeconds(timeBetweenBullet);
        }
        StopCoroutine(FireMultipleBullet());
    }
}
