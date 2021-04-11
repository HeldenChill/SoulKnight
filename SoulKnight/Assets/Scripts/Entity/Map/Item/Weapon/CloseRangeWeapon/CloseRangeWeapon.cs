using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CloseRangeWeapon : Weapon
{
    protected const string SWING = "Swing";
    protected MeleeAttackModule meleeAttackModule;
    public int damageWeapon = 2;

    protected override void Awake(){
        base.Awake();
        meleeAttackModule = GetComponent<MeleeAttackModule>();
    }
}
