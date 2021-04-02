using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangePlayer : RigidbodyObject
{
    public static int layer;
    protected Vector3 initPlayerScale;
    protected Vector3 initGunScale;
    public GameObject gun;
    protected virtual void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        gameObject.layer =LayerMask.NameToLayer("Player");
        layer = LayerMask.GetMask("Player");
        Debug.Log(layer);
        initPlayerScale = this.gameObject.transform.localScale;
        initGunScale = gun.transform.localScale;
    }

    // Update is called once per frame

    protected virtual void Update()
    {
        move();
        if(Input.GetMouseButton(0)){
            attack();
        }
        
        
        lookAt(Vector2.right,HelperClass.getMouse2DPosition());
    }

    protected override Vector2 move()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        input = input.normalized;
        velocity = input * speed;
        rb.velocity = velocity;
        return velocity;
    }
    protected override void attack()
    {
        gun.GetComponent<Gun>().attack();            
    }
    protected override void playAnimation()
    {
        
    }
    protected override void getDamage(int damage)
    {
        throw new System.NotImplementedException();
    }

    protected override void destroy()
    {
        throw new System.NotImplementedException();
    }

    
    private void lookAt(Vector2 face,Vector2 point){
        Vector2 thisPosition = new Vector2(transform.position.x,transform.position.y); //the position of player(Convert from Vector3->Vector2)
        Vector2 vectorToPoint = point - thisPosition;   //vectorToPoint through 2 point,the position of player and the position of mouse(HelperClass.getMouse2DPosition())
        if(point.x < thisPosition.x){
            this.gameObject.transform.localScale = new Vector3(initPlayerScale.x * -1,initPlayerScale.y,initPlayerScale.z);
            gun.transform.localScale = new Vector3(initGunScale.x * -1,initGunScale.y * -1,initGunScale.z);
        }
        else{
            this.gameObject.transform.localScale = initPlayerScale;
            gun.transform.localScale = initGunScale;
        }
        HelperClass.rotateObject(face,vectorToPoint,gun);
    }
}
