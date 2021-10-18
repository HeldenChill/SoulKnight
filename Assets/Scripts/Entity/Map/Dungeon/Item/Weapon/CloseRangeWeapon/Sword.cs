using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : CloseRangeWeapon
{
    // Start is called before the first frame update

    protected override void Awake()
    {
        base.Awake();
        reloadTime = 0.5f;
        damageWeapon = 2;
        manaToUse = 1;
    }
    public override int attack(Vector2 target)
    {
        mechanism(target);
        return 0;

    }

    protected override void mechanism(Vector2 target)
    {
        animatorModule.playAnimation(SWING,true);
    }

    public override void setLayer(){

    }

    public void dealDamage(){
        meleeAttackModule.doAttackMechanism(damageWeapon);
    }
}
