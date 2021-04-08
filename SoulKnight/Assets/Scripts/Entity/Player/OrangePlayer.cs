using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangePlayer : MonoBehaviour,ICharacterBase
{
    public static GameObject player;
    public static int layerMask;
    public int speed = 5;
    public int hp = 5;
    public int mana = 100;
    [SerializeField]private int money = 50;
    private FilpForDirectionView filpForDirection;
    private KeyboardInput inputModule;
    private MoveVelocityRBModule moveModule;
    private ContactItemModule contactItemModule;
    private GameObject weapon;

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
            if(filpForDirection != null)
                filpForDirection.Weapon = weapon;
        }
    }
    
    protected virtual void Start()
    {
        player = gameObject;
        inputModule = GetComponent<KeyboardInput>();
        moveModule = GetComponent<MoveVelocityRBModule>();
        filpForDirection = GetComponent<FilpForDirectionView>();
        contactItemModule = GetComponent<ContactItemModule>();

        gameObject.layer = LayerMask.NameToLayer("Player");
        layerMask = LayerMask.GetMask("Player");

        weapon = GameHelper.findWeapon(this.gameObject);
    }

    void FixedUpdate(){
        moveModule.setVelocity(inputModule.MoveKeyBoard * speed);
    }

    protected virtual void Update()
    {
        
        if(Input.GetMouseButton(0)){
            weapon.GetComponent<Weapon>().attack(HelperClass.getMouse2DPosition());   
        } 
        if(inputModule.EquipItem && contactItemModule.ItemInRange[0] != null){
            //Debug.Log(contactItemModule.ItemInRange[0].gameObject.name);
            contactItemModule.ItemInRange[0].gameObject.GetComponent<IItem>().getItem();
        }
        filpForDirection.lookAt(HelperClass.getMouse2DPosition());
    }

    public void changeWeapon(GameObject weapon){
        Weapon = weapon;
    }
    public void getDamage(int damage){
        hp -= damage;
        if(hp <= 0){
            die();
        }
    }

    public void getMana(int mana){
        this.mana += mana; 
    }
    public int getLayer(){
        return gameObject.layer;
    }
    public int getLayerMask(){
        return LayerMask.GetMask(LayerMask.LayerToName(getLayer()));
    }

    private void die(){

    }

}
