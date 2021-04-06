using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangePlayer : MonoBehaviour
{
    public static GameObject player;
    public static int layer;
    public float speed = 5;
    
    private FilpForDirectionView filpForDirection;
    private KeyboardInput inputModule;
    private MoveVelocityRBModule moveModule;
    private GameObject gun;
    protected virtual void Start()
    {
        player = gameObject;
        inputModule = GetComponent<KeyboardInput>();
        moveModule = GetComponent<MoveVelocityRBModule>();
        filpForDirection = GetComponent<FilpForDirectionView>();


        gameObject.layer =LayerMask.NameToLayer("Player");
        layer = LayerMask.GetMask("Player");

        gun = GameMechanism.findWeapon(this.gameObject);
    }


    protected virtual void Update()
    {
        moveModule.setVelocity(inputModule.MoveKeyBoard * speed);
        if(Input.GetMouseButton(0)){
            gun.GetComponent<Weapon>().attack(HelperClass.getMouse2DPosition());   
        } 
        filpForDirection.lookAt(HelperClass.getMouse2DPosition());
    }


}
