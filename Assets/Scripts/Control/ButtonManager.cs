using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private GameObject MainCanvas;
    [SerializeField]private GameObject[] SubCanvas;
    public void setActivePauseGUI(bool isActive){
        if(isActive){
            Time.timeScale = 0f;
            MainCanvas.SetActive(true);
        }
        else{
            Time.timeScale = 1f;
            MainCanvas.SetActive(false);
        }
    }

    public void onContinueButtonClicked(){
        setActivePauseGUI(false);
    }

    public void onSaveButtonClicked(){
        Debug.Log("Save");
    }

    public void onOptionButtonClicked(){
        if(SubCanvas != null){
            SubCanvas[0].SetActive(true);
            MainCanvas.SetActive(false);
        } 
    }

    public void onExitButtonClicked(){
        Debug.Log("Exit");
    }

    public void onNewGameButtonClicked(){
        Scenes.current.Load("WaitingRoom");
    }

    public void onContinueGameButtonClicked(){
        Debug.Log("Continue Game");
    }

    public void onBackButtonClicked(){
        if(SubCanvas != null){
            SubCanvas[0].SetActive(true);
            MainCanvas.SetActive(false);
        } 
    }

}
