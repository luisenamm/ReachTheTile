using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This code helps us to manage the pause menu

public class PauseManager : MonoBehaviour
{
    public GameObject PauseCanvas;
    public static bool isPaused;

    public MoveCube cube;
    // Start is called before the first frame update
    void Start()
    {
        //We start making false the pause canvas
        isPaused=false;
        PauseCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //As we use the same button to pause and restart the game, we need to be sure that the user has won
        //to avoid having mistakes with the pause menu
        if(!MoveCube.won){
            if ((Input.GetKeyUp(KeyCode.JoystickButton0))|| (Input.GetKeyUp(KeyCode.P))){            
                Pause();
            }
        }
        
        //If it is paused, we can Rstart, Quit and see the instructions of the game
        if(isPaused){
            //B
            if (Input.GetKeyUp(KeyCode.JoystickButton13) || (Input.GetKeyUp(KeyCode.Q) )){
                Application.Quit();
            }

            //X
            if (Input.GetKeyUp(KeyCode.JoystickButton15) || (Input.GetKeyUp(KeyCode.R))  ){
                isPaused=false;
                PauseCanvas.SetActive(false);
                cube.RestartVariables();  
            }
             
             if (Input.GetKeyUp(KeyCode.JoystickButton14) || (Input.GetKeyUp(KeyCode.P) )){
                Pause();
            }
        }
    }
    

    public void Pause(){
        isPaused=!isPaused;
        if(isPaused){
            PauseCanvas.SetActive(true);
            Time.timeScale=0;
        }else{
            PauseCanvas.SetActive(false);
            Time.timeScale=1;
        }
    }
}
