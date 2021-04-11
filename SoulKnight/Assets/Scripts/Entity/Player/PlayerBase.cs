using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public static GameObject player;
    public static int layerMask;
    public int speed = 5;
    public int hp = 5;
    public int mana = 100;
    [SerializeField]private int money = 50;
    protected LookAtModule lookAtModule;
    protected KeyboardInput inputModule;
    protected MoveVelocityRBModule moveModule;
    protected ContactItemModule contactItemModule;
    protected GameObject weapon;

    public int Hp{
        get{return hp;}
        set{
            if(value <= 0){
                hp = 0;
                die();
            }
            else{
                hp = value;
            }
        }
    }
    public int Money{
        get{return money;}
        set{
            money = value;
        }
    }
    public GameObject Weapon{
        get{return weapon;}
        set{
            weapon = value;
        }
    }

    public int Mana{
        get{return mana;}
        set{
            mana = value;
        }
    }

    public int Speed{
        get{return speed;}
        set{
            speed = value;
        }
    }
    protected virtual void Awake()
    {
        if(TryGetComponent<KeyboardInput>(out KeyboardInput _inputModule)){
            inputModule = _inputModule;
        }
        else{
            Debug.LogError(player.name + " dont have " + typeof(KeyboardInput).Name);
        }

        if(TryGetComponent<LookAtModule>(out LookAtModule _lookAtModule)){
            lookAtModule = _lookAtModule;
        }
        else{
            Debug.LogError(player.name + " dont have " + typeof(LookAtModule).Name);
        }

        if(TryGetComponent<MoveVelocityRBModule>(out MoveVelocityRBModule _moveModule)){
            moveModule = _moveModule;
        }
        else{
            Debug.LogError(player.name + " dont have " + typeof(MoveVelocityRBModule).Name);
        }

        if(TryGetComponent<ContactItemModule>(out ContactItemModule _contactItemModule)){
            contactItemModule = _contactItemModule;
        }
        else{
            Debug.LogError(player.name + " dont have " + typeof(ContactItemModule).Name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void resetPosition(){
        transform.position = Vector3.zero;
    }

    public void getDamage(int damage){
        Hp -= damage;
    }

    public void getMana(int mana){
        Mana += mana; 
    }

    protected void die(){

    }
}
