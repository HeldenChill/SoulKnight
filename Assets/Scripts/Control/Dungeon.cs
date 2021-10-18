using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    [SerializeField]private GameObject player;
    [SerializeField]private CameraFollow cameraFollow;
    void Start()
    {
        Debug.Log(Scenes.current.playerProfile.getInfo()); 
        GameObject playerInstance = Instantiate(player,new Vector3(0,0,-10),Quaternion.identity);
        playerInstance.GetComponent<PlayerBase>().setPlayerBaseProfile(Scenes.current.playerProfile);
        cameraFollow.followedObject = playerInstance.transform;
    }
}
