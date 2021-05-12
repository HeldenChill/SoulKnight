using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public static Scenes current;
    void Awake(){
        Debug.Log(this);
        if(current == null){
            DontDestroyOnLoad(gameObject);
            current = this;   
        }  
        else if(current != this){
            Destroy(gameObject);
        }
    }

    private static Dictionary<string, GameObject> parameters;
    public PlayerBaseProfile playerProfile;
    public void Load(string sceneName, Dictionary<string, GameObject> parameters = null) {
        Scenes.parameters = parameters;
        SceneManager.LoadScene(sceneName);
    }

    public void Load(string sceneName, GameObject player){
        playerProfile = player.GetComponent<PlayerBase>().getProfile();
        SceneManager.LoadScene(sceneName);
    }
 
    public PlayerBaseProfile getPlayer() {
        return playerProfile;
    }
}
