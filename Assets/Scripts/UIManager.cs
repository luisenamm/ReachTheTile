using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public string SceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.JoystickButton0) || Input.GetKeyUp(KeyCode.JoystickButton14)){
            LoadScene();
        }
        

        
    }

    public void Restart(){
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(){
         SceneManager.LoadScene(SceneName);
    }
}
