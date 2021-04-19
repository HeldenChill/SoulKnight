using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBase : MonoBehaviour
{
    public static GameObject player;
    public static int layerMask;
    
    [SerializeField]protected int speed = 5;
    protected int hp = 5;
    protected int shield = 5;
    protected int mana = 100;
    [SerializeField]protected int maxHp;
    [SerializeField]protected int maxShield;
    [SerializeField]protected int maxMana;
    [SerializeField]protected int money = 50;
    [SerializeField]protected PlayerGUIControl playerGUI;
    protected LookAtModule lookAtModule;
    protected KeyboardInput inputModule;
    protected IMoveBase moveModule;
    protected ContactItemModule contactItemModule;
    protected GameObject weapon;

    public int Hp{
        get{return hp;}
        set{
            if(value <= 0){
                hp = 0;
                playerGUI.HpBar.setValue(0);
                die();
            }
            else if(value <= maxHp){
                hp = value;
            }
            else{
                hp = maxHp;
            }
        }
    }

    public int Shield{
        get{return shield;}
        set{
            if(value < 0){
                Hp += value;
                shield = 0;
                playerGUI.HpBar.setValue(Hp);
            }
            else if(value <= maxShield){
                shield = value;
            }
            else{
                shield = maxShield;
            }
            playerGUI.ShieldBar.setValue(shield);
        }
    }

    public int Mana{
        get{return mana;}
        set{
            if(value < 0){
                mana = 0;
            }
            else if(value <= maxMana){
                mana = value;
            }
            else{
                mana = maxMana;
            }
            
            playerGUI.ManaBar.setValue(mana);
        }
    }
    public int Money{
        get{return money;}
        set{
            money = value;
            playerGUI.moneyText.text = money.ToString(); 
        }
    }
    public GameObject Weapon{
        get{return weapon;}
        set{
            weapon = value;
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

        if(TryGetComponent<IMoveBase>(out IMoveBase _moveModule)){
            moveModule = _moveModule;
        }
        else{
            Debug.LogError(player.name + " dont have " + typeof(IMoveBase).Name);
        }

        if(TryGetComponent<ContactItemModule>(out ContactItemModule _contactItemModule)){
            contactItemModule = _contactItemModule;
        }
        else{
            Debug.LogError(player.name + " dont have " + typeof(ContactItemModule).Name);
        }
        hp = maxHp;
        shield = maxShield;
        mana = maxMana;
        setupGUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void resetPosition(){
        transform.position = Vector3.zero;
    }

    public void getDamage(int damage){
        Shield -= damage;
    }

    public void getMana(int mana){
        Mana += mana; 
    }
    protected abstract void skill();
    protected abstract void endSkill();

    protected abstract void die();
    private void setupGUI(){
        playerGUI.HpBar.setMaxValue(Hp);
        playerGUI.ShieldBar.setMaxValue(Shield);
        playerGUI.ManaBar.setMaxValue(Mana);
        playerGUI.moneyText.text = money.ToString();
    }
}
