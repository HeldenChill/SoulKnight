﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour, ICharacterBase
{
    // Start is called before the first frame update
    [HideInInspector]
    public int RoomInID = -1;
    public float speed = 1;
    public int hp = 20;
    protected Vector2 direction;
    protected Transform target;
    protected MoveVelocityRBModule moveModule;
    protected SensorModule sensor;
    protected LookAtModule lookAtModule;
    protected GameObject weapon;
    protected Weapon weaponScripts;
    public GameObject Weapon
    {
        get => weapon;
        set
        {
            weapon = value;
            weaponScripts = weapon.GetComponent<Weapon>();
        }
    }
    public int Hp{
        get{return hp;}
        set{
            if(hp <= 0){
                Die();
                hp = 0;
            }
            else{
                hp = value;
            }
        }
    }
    protected virtual void Awake()
    {
        if(TryGetComponent<LookAtModule>(out LookAtModule _lookAtModule)){
            lookAtModule = _lookAtModule;
        }
        else{
            Debug.LogError(name + " dont have " + typeof(LookAtModule).Name);
        }

        if(TryGetComponent<MoveVelocityRBModule>(out MoveVelocityRBModule _moveModule)){
            moveModule = _moveModule;
        }
        else{
            Debug.LogError(name + " dont have " + typeof(MoveVelocityRBModule).Name);
        }

        if(TryGetComponent<SensorModule>(out SensorModule _sensor)){
            sensor = _sensor;
        }
        else{
            Debug.LogError(name + " dont have " + typeof(SensorModule).Name);
        }
    }

    protected void trigger(int id){
        if(id == this.RoomInID){
            //this.isTrigger = true;
            this.enabled = true;
        }
    }
    protected void SetRandomDirectionMove(float minAngle,float maxAngle){
        direction = HelperClass.getRandomDirection(minAngle,maxAngle);
    }

    public virtual void GetDamage(int damage){
        Hp -= damage;
    }
    public void ChangeWeapon(GameObject weapon)
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            GetDamage(other.GetComponent<Bullet>().Damage);
        }
    }
    //die
    protected virtual void Die()
    {
        EventManager.Inst.createNormalReward(transform.position, 1);
        EventManager.Inst.EnemyDie(RoomInID);
        Destroy(gameObject);
    }

    public int GetLayer()
    {
        return gameObject.layer;
    }

    public int GetLayerMask()
    {
        return LayerMask.GetMask(LayerMask.LayerToName(GetLayer()));
    }
}
