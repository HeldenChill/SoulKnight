using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : RigidbodyObject
{
    public static Camera mainCamera;
    public GameObject[] bullet;
    private float rotationSpeed = 2f;
    private GameObject turret;
    private GameObject body;
    private float rotation = 0;
    public override void Init(float hp = 50,float speed = 5){
        base.Init(hp,speed);
    }

    protected virtual void Start(){
        this.Init();

        rb = gameObject.GetComponent<Rigidbody2D>();    
        turret = gameObject.transform.GetChild(0).gameObject;
        body = gameObject.transform.GetChild(1).gameObject;
        mainCamera = Camera.main;
    }
    protected virtual void Update(){
        if(Input.GetMouseButtonDown(0)){
            attack();
        }
    }
    protected virtual void FixedUpdate(){
        move();
        rb.velocity = this.velocity;
        lookAt(Vector2.right,HelperClass.getMouse2DPosition());
    }

    protected override Vector2 move(){
        float isMove = Input.GetAxisRaw("Vertical");
        float isRotate = Input.GetAxisRaw("Horizontal");
        //Debug.Log("isRotate = " + isRotate.ToString() + ", rotation = " + rotation.ToString());
        rotation = gameObject.transform.rotation.eulerAngles.z;
        if(isRotate != 0){
            rotation += -Mathf.Sign(isRotate) * rotationSpeed;
        }
        gameObject.transform.rotation = Quaternion.Euler(0,0,rotation);
        this.velocity = HelperClass.angleToVector(rotation) * speed * isMove;
        return this.velocity;
    }
    protected override void attack()
    {
        fire(bullet[0]);
    }
    
    protected override void getDamage(int damage){

    }

    protected override void playAnimation(){

    }


    protected override void destroy()
    {
       
    }

    private void lookAt(Vector2 face,Vector2 point){
        Vector2 thisPosition = new Vector2(transform.position.x,transform.position.y); //the position of player(Convert from Vector3->Vector2)
        Vector2 vectorToPoint = point - thisPosition;   //vectorToPoint through 2 point,the position of player and the position of mouse(HelperClass.getMouse2DPosition())
        HelperClass.rotateObject(face,vectorToPoint,turret);
    }

    private void fire(GameObject bullet){
        GameObject gun = turret.transform.GetChild(0).gameObject;
        Vector3 posOfFireObj = gun.transform.GetChild(0).position;
        Vector2 firePoint = new Vector2(posOfFireObj.x,posOfFireObj.y);
        Vector2 fireDirection = (HelperClass.getMouse2DPosition() - (Vector2)(turret.transform.position)).normalized;
        GameObject instBullet = Instantiate(bullet,firePoint,HelperClass.getQuaternion2Vector(Vector2.right,fireDirection));
        instBullet.GetComponent<Bullet>().fire();
    }

    
    void OnCollisionEnter2D(Collision2D col)
    {
       // Debug.Log("OnCollisionEnter2D");
    }
}