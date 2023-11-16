using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private GameObject mana;
    [SerializeField]private GameObject coin;
    public void createNormalReward(Vector3 position,int level){
        int numOfObject = UnityEngine.Random.Range(0,level + 3);
        for(int i = 0; i < numOfObject; i++){
            Instantiate(mana,position,Quaternion.identity,EventManager.Inst.Environment.transform);
            Instantiate(coin,position,Quaternion.identity,EventManager.Inst.Environment.transform);
        }
    }
}
