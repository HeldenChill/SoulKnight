using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] items;
    private GameObject[] tables;
    private Vector3[] positionsOfTable;
    private bool isTrigger = true;
    void Start()
    {
        tables = new GameObject[5];
        positionsOfTable = new Vector3[5];
        for(int i = 1; i <= 4; i++){
            tables[i] = transform.GetChild(i).gameObject;
            positionsOfTable[i] = transform.GetChild(i).transform.position;
            if(items[i-1] != null){
                items[i-1] = Instantiate(items[i-1],positionsOfTable[i],Quaternion.identity,EventManager.Inst.Environment.transform);
                items[i-1].GetComponent<IItem>().SetActiveContact(false);
            }
                
        }
    }

    // Update is called once per frame
    public void playerBuyItem(int idTable,GameObject player){
        if(player != null && idTable >= 1 && idTable <= 4){
            int cost = items[idTable - 1].GetComponent<IItem>().GetValue();
            if(player.GetComponent<OrangePlayer>().Money > cost){
                player.GetComponent<OrangePlayer>().Money -= cost;
                items[idTable - 1].GetComponent<IItem>().SetActiveContact(true);
                tables[idTable].GetComponent<IItem>().SetActiveContact(false);
            }   
            else{
                Debug.Log("Not enough Money");
            }
        }
    }
}
