using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LongRangeWeapon : Weapon
{
    
    protected const string FIRE = "Fire";
    protected const string IDLE = "Idle";
    protected static int valueRecoil = 100;
    public float accurancy = 7f;   
    protected abstract void Recoil();
}
