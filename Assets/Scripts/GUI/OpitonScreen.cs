using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpitonScreen : MonoBehaviour
{
    [SerializeField] private BarControl soundControl;
    
    void Awake(){
        soundControl.setMaxValue(1);
        soundControl.setValue(AudioManager.current.Volume);
    }

    public void onSoundChange(){
        AudioManager.current.Volume = soundControl.getValue();
    }
}
