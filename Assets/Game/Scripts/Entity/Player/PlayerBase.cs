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
    protected InputModule inputModule;
    protected IMoveBase moveModule;
    protected ContactItemModule contactItemModule;
    protected GameObject weapon;
    protected Weapon weaponScripts;

    public int Hp{
        get{return hp;}
        set{
            if(value <= 0){
                hp = 0;
                if(playerGUI != null)
                    playerGUI.HpBar.setValue(0);
                Die();
            }
            else if(value <= maxHp){
                hp = value;
                if(playerGUI != null)
                    playerGUI.HpBar.setValue(hp);
            }
            else{
                hp = maxHp;
                if(playerGUI != null)
                    playerGUI.HpBar.setValue(maxHp);
            }
        }
    }

    public int Shield{
        get{return shield;}
        set{
            if(value < 0){
                Hp += value;
                shield = 0;
                if(playerGUI != null)
                    playerGUI.HpBar.setValue(Hp);
            }
            else if(value <= maxShield){
                shield = value;
            }
            else{
                shield = maxShield;
            }
            if(playerGUI != null)
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
            if(playerGUI != null)
                playerGUI.ManaBar.setValue(mana);
        }
    }
    public int Money{
        get{return money;}
        set{
            money = value;
            if(playerGUI != null)
                playerGUI.moneyText.text = money.ToString(); 
        }
    }
    public GameObject Weapon{
        get{return weapon;}
        set{
            weapon = value;
            weaponScripts = weapon.GetComponent<Weapon>();
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
        if(TryGetComponent<InputModule>(out InputModule _inputModule)){
            inputModule = _inputModule;
        }
        else{
            Debug.LogError(player.name + " dont have " + typeof(InputModule).Name);
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
        
        initAttribute();
        initGUI();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(inputModule.OptionMenu){
            EventManager.Inst.activePauseMenu();
        }
    }
    public void resetPosition(){
        transform.position = Vector3.zero;
    }

    public void GetDamage(int damage){
        Shield -= damage;
    }

    public void getMana(int mana){
        Mana += mana; 
    }

    public PlayerBaseProfile getProfile(){
        PlayerBaseProfile profile = new PlayerBaseProfile();
        profile.maxHp = maxHp;
        profile.maxMana = maxMana;
        profile.maxShield = maxShield;
        profile.Hp = Hp;
        profile.Mana = Mana;
        profile.Shield = Shield;
        profile.Speed = Speed;
        return profile;
    }

    public void setPlayerBaseProfile(PlayerBaseProfile profile){
        maxHp = profile.maxHp;
        maxMana = profile.maxMana;
        maxShield = profile.maxShield;
        Hp = profile.Hp;
        Mana = profile.Mana;
        Shield = profile.Shield;
        Speed = profile.Speed;
        initGUI();
    }
    protected abstract void Skill();
    protected abstract void EndSkill();

    protected abstract void Die();
    private void initAttribute(){
        hp = maxHp;
        shield = maxShield;
        mana = maxMana;
    }

    private void initGUI(){
        if(playerGUI == null) return;       //If player not in the dungeon(=> playerGUI = null)
        playerGUI.HpBar.setMaxValue(maxHp);
        playerGUI.ShieldBar.setMaxValue(maxShield);
        playerGUI.ManaBar.setMaxValue(maxMana);

        playerGUI.HpBar.setValue(Hp);
        playerGUI.ShieldBar.setValue(Shield);
        playerGUI.ManaBar.setValue(Mana);
        playerGUI.moneyText.text = money.ToString();
    }


}
