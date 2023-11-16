using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour,IItem
{
    // Start is called before the first frame update
    public float reloadTime = 1f;
    public int value = 18;
    protected bool isLookAt = true;
    public LookAtModule lookAtModule;
    public AnimatorModule animatorModule;
    public ContactPlayerModule contactPlayer;
    public ChangeWeaponModule changeWeapon;
    protected int manaToUse;
    protected virtual void Awake(){
        if(TryGetComponent<LookAtModule>(out LookAtModule _lookAtModule)){
            lookAtModule = _lookAtModule;
        }
        else{
            Debug.LogError(name + " don't have" + typeof(LookAtModule).Name);
        }

        if(TryGetComponent<AnimatorModule>(out AnimatorModule _animatorModule)){
            animatorModule = _animatorModule;
        }
        else{
            Debug.LogError(name + " don't have" + typeof(AnimatorModule).Name);
        }

        if(TryGetComponent<ContactPlayerModule>(out ContactPlayerModule _contactPlayer)){
            contactPlayer = _contactPlayer;
        }
        else{
            Debug.LogError(name + " don't have" + typeof(ContactPlayerModule).Name);
        }

        if(TryGetComponent<ChangeWeaponModule>(out ChangeWeaponModule _changeWeapon)){
            changeWeapon = _changeWeapon;
        }
        else{
            Debug.LogError(name + " don't have" + typeof(ChangeWeaponModule).Name);
        }
    }

    protected virtual void Start(){
        lookAtModule.IsWeapon = true;
        if(transform.parent != null && transform.parent.gameObject.layer != LayerMask.NameToLayer("Environment")){
            contactPlayer.Active = false;
            isLookAt = true;
        }
    }
    public virtual void GetItem(){
        if(contactPlayer.PlayerInRange[0] != null){
            GameObject weaponOfPlayer = GameHelper.FindWeapon(contactPlayer.PlayerInRange[0].gameObject);
            changeWeapon.ChangeWeapon(weaponOfPlayer);
        }
    }
    public virtual int GetValue()
    {
        return value;
    }
    public virtual void SetActiveContact(bool isActive)
    {
        contactPlayer.Active = isActive;
    }
    public virtual void Aim(Vector2 target){
        if(isLookAt){
            lookAtModule.LookAt(target);
        }
    }

    public abstract int Attack(Vector2 target);
    protected abstract void Mechanism(Vector2 target);
    public abstract void SetLayer();

}
