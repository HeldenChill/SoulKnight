using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpitonScreen : MonoBehaviour
{
    [SerializeField] private BarControl soundControl;
    
    void Awake(){
        soundControl.setMaxValue(1);
        soundControl.setValue(AudioManager.Inst.Volume);
    }

    public void onSoundChange(){
        AudioManager.Inst.Volume = soundControl.getValue();
    }
}
