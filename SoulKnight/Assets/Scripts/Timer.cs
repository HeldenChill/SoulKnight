using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    
    public delegate void ObjectTimeOut();
    public event ObjectTimeOut TimeOut;

    private bool timerIsStart = false;
    private float timeRemaining = 0;
    private float timeAccuracy = 0.05f;
    private Coroutine lastRoutine = null;
    public float TimeRemaining
    {
        get { return timeRemaining; }
    } 
    public float TimeAccuracy{
        get{ return timeAccuracy; }
        set{ timeAccuracy = value; }
    }
    public bool TimerIsStart{
        get{return timerIsStart;}
    }
    public void timeStart(float time){
        timerIsStart = true;
        timeRemaining = time;
        lastRoutine = StartCoroutine(subtractTime());
    }

    private IEnumerator subtractTime(){
        while(timeRemaining > 0){
            //Debug.Log(timeRemaining);
            timeRemaining -= timeAccuracy;
            yield return new WaitForSeconds(timeAccuracy);
        }

        timeOut();
        timeStop();
    }
    public void timeStop(){
        timerIsStart = false;
        timeRemaining = 0;
        if(lastRoutine != null)
            StopCoroutine(lastRoutine);
    }
    private void timeOut(){
        if(TimeOut != null){
            TimeOut();
        }
    }
    void OnDestroy(){
        timeStop();
    }
} 