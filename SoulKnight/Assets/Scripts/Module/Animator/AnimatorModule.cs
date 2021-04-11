﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AnimatorModule : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    bool isPlay = false;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public float playAnimation(string name){
        animator.Play(name,-1,0);
        return animator.GetCurrentAnimatorStateInfo(0).length;
    }

    public float playAnimation(string name,bool isPLayAll = false){
        if(!isPlay){
            if(!isPLayAll){
                playAnimation(name);
            }
            else{
                isPlay = true;
                playAnimation(name);
                Invoke("activePlayAnimation",animator.GetCurrentAnimatorStateInfo(0).length);
            }
        }
        return animator.GetCurrentAnimatorStateInfo(0).length;
    }

    private void activePlayAnimation(){
        isPlay = false;
    }
}
