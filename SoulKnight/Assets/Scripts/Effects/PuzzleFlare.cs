using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleFlare : MonoBehaviour
{
    private const string FIRE = "Fire";
    LookAtModule lookAtModule;
    AnimatorModule animatorModule;
    void Start(){
        animatorModule = GetComponent<AnimatorModule>();
        lookAtModule = GetComponent<LookAtModule>();
    }
    public void activeFlare(){
        animatorModule.playAnimation(FIRE);
    }
    void Update(){
        lookAtModule.lookAt(HelperClass.getMouse2DPosition());
    }

}
