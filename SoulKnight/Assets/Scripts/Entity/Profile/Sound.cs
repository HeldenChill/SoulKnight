using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    public string name;
    public AudioClip clip;
    [Range(0f,1f)]
    [SerializeField] private float volume;
    [Range(.1f,3f)]
    [SerializeField] private float pitch;

    public float Volume{
        get{return volume;}
        set{
            volume = value;
            source.volume = volume;
        }
    }

    
    public float Pitch{
        get{return pitch;}
        set{
            pitch = value;
            source.pitch = volume;
        }
    }

    [HideInInspector]
    public AudioSource source;

}
