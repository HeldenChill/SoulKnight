using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Sound[] sounds;
    public static AudioManager Inst;
    [Range(0f,1f)]
    private float volume = 1;
    public float Volume{
        get{return volume;}
        set{
            volume = value;
            foreach(var sound in sounds){
                sound.Volume = volume;
            }
        }
    }
    void Awake(){
        if(Inst == null){
            Inst = this;
            foreach(var sound in sounds){
                AudioSource s = gameObject.AddComponent<AudioSource>();
                s.clip = sound.clip;
                s.volume = sound.Volume;
                s.pitch = sound.Pitch;
                sound.source = s;
            }
        }
        else{
            Destroy(this);
        }
        
    }

    public void Play(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}
