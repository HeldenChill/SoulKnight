using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour,IItem
{
    // Start is called before the first frame update
    public ContactPlayerModule contactPlayer;
    public ChangeWeaponModule changeWeapon;
    public abstract void attack(Vector2 target);
    public abstract void mechanism(Vector2 target);
    public abstract void setLayer();
    public abstract void getItem();
    public abstract int getValue();
    public abstract void setActiveContact(bool isActive);
}
