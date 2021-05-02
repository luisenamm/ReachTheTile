using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This code helps us to manage the simple actions of the UI


public class UIManager : MonoBehaviour
{
    public string SceneName;
    
    void Update()
    {
        //Here we load the next scene of the game
        if (Input.GetKeyUp(KeyCode.JoystickButton0) || Input.GetKeyUp(KeyCode.JoystickButton14)){
            LoadScene();
        }
        
        
    }

    //Here we restart the current scene
    public void Restart(){
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(){
         SceneManager.LoadScene(SceneName);
    }
}
