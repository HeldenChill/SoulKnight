using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperClass
{
    public static Vector2 getMouse2DPosition(){
        Vector3 worldPosition;
        Vector3 mousePos = Input.mousePosition;
        Camera mainCamera = Camera.main;
        //mousePos.z = Player.mainCamera.nearClipPlane;
        //worldPosition = Player.mainCamera.ScreenToWorldPoint(mousePos); //Draw a line through 2 point,center of camera and mousePos
        mousePos.z = mainCamera.nearClipPlane;
        worldPosition = mainCamera.ScreenToWorldPoint(mousePos);
        return new Vector2(worldPosition.x,worldPosition.y); //Convert the position to 2D
    }
    public static void rotateObject(Vector2 vec1,Vector2 vec2,GameObject gObject){
        Quaternion quaternion = getQuaternion2Vector(vec1,vec2);
        gObject.transform.rotation = quaternion;
    }

    public static Quaternion getQuaternion2Vector(Vector2 vec1,Vector2 vec2){
        float angle = Vector2.SignedAngle(vec1,vec2); //angle is the angle between vector face and vector vectorToPoint
        Quaternion quaternion = Quaternion.Euler(0,0,angle); // Convert from Euler to Quaternion
        return quaternion;
    }

    public static Quaternion getQuaternionVector3(Vector3 vec){
        return Quaternion.Euler(vec.x,vec.y,vec.z);
    }

    public static Vector2 angleToVector(float angle){
        while(angle < 0){
            angle += 360;
        }

        while(angle >= 360){
            angle -= 360;
        }


        float vecX = Mathf.Cos(Mathf.Deg2Rad * angle);
        float vecY = Mathf.Sin(Mathf.Deg2Rad * angle);
        return new Vector2(vecX,vecY);
    }

    public static void flip(GameObject gameObject){
        Vector3 initialScale = gameObject.transform.localScale; 
        initialScale.x *= -1;
        gameObject.transform.localScale = initialScale;
    }

    //Random for AI Enemy
    public static Vector2 getRandomVector2D(bool yGreater0,bool xGreater0){
        float valX = Random.Range(-1f,1f);
        float valY = Random.Range(1f,1f);
        
        if(xGreater0)
            while(valX < 0)
                valX = Random.Range(-1f,1f);
        else
            while(valX >= 0)
                valX = Random.Range(-1f,1f);
                
            

        if(yGreater0)
            while(valY < 0)
                valY = Random.Range(-1f,1f);
        else
            while(valY >= 0)
                valY = Random.Range(-1f,1f); 
        return new Vector2(valX,valY).normalized;
    }

    public static Vector2 getRandomVector2D(){
        float valX = Random.Range(-1f,1f);
        float valY = Random.Range(-1f,1f);     
        return new Vector2(valX,valY).normalized;
    }
    public static Vector2 getRandomDirection(float min = 0,float max = 360){
        float angle = Random.Range(min,max);
        return angleToVector(angle);
    }
    
}
