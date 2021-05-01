using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject PauseCanvas;
    public static bool isPaused;

    public MoveCube cube;
    public string SceneName;
    // Start is called before the first frame update
    void Start()
    {
        PauseCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.JoystickButton0) || (Input.GetKeyUp(KeyCode.P) )){
            Pause();
        }
        
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

    public void LoadScene(){
         SceneManager.LoadScene(SceneName);
    }
}
