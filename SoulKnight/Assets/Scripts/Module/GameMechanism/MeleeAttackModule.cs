using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackModule : MonoBehaviour
{
    public Transform attackPosition;
    public float radiusAttack;

    void Awake(){
        attackPosition = transform.GetChild(0).GetChild(0).transform;
    }
    public void doAttackMechanism(int damage){
        Collider2D[] targets = Physics2D.OverlapCircleAll(attackPosition.position,radiusAttack,LayerMask.GetMask("Enemy"));
        for(int i = 0; i < targets.Length; i++){
            targets[i].GetComponent<EnemyBase>().getDamage(damage);
            Debug.Log(targets[i].name);
        }
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position,radiusAttack);
    }
}
