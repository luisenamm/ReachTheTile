using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MoveCube : MonoBehaviour
{
    public int _rotationSpeed = 15;

    public float FBForce = 250.0f;
    public float LRForce = 200.0f;

    public float _waitTime= 0.1f;

    public float YposIncrement = 0.1f; 
    
    float zIncrement;
    float xIncrement;
    Vector3 tempPos;

    private float Zpos;

    public float xOffset=0.25f; 
    public float zOffset=0.1f; 
   
    public float resetRotationTime;

    private bool canRotate;

    public static bool won;
    public GameObject winnerScene;
    public GameObject MuteCanvas;

    private AudioSource audio;
    public AudioClip ForwardSound;
    public AudioClip BackSound;
    public AudioClip RightSound;
    public AudioClip LeftSound;
    public AudioClip WinnerSound;

    private bool isMuted;
    private bool soundPlayed;

    // Start is called before the first frame update
    void Start()
    {
        RestartVariables();    
        canRotate=true;  
        audio=GetComponent<AudioSource>();
        won=false;
        isMuted=false;
        MuteCanvas.SetActive(false);
        soundPlayed=false;
        
    }

    // Update is called once per frame
    void Update()
    {
        //We fisrt check that the game is not paused in order to move the cube
        if(!PauseManager.isPaused){

            //We can restart the level        
            if (Input.GetKeyUp(KeyCode.R) || (Input.GetKeyUp(KeyCode.JoystickButton0))){
                RestartVariables();  
            }            

            if(won){
                //If the user has won, he can restart the level and play a new one        
                if((Input.GetKeyUp(KeyCode.JoystickButton0)) ||Input.GetKeyUp(KeyCode.R)) Restart();

            }

            if((Input.GetKeyUp(KeyCode.JoystickButton6))){
                Mute();
            }

            
            Vector3 currPos = this.transform.position;

            Zpos=(float)System.Math.Floor(this.transform.position.z);

            //Forward movement
            if ((Input.GetKeyUp(KeyCode.W ) || (Input.GetKeyUp(KeyCode.JoystickButton12))) && currPos.z < 5.9f && canRotate) {
                //If it is not muted, we play a sound as feedback to the user to give him 
                //feedback of where id he moving
                if(!isMuted) audio.PlayOneShot(ForwardSound, 1.0f);
                canRotate=false;
                //We increase a z value to move the cube one unit each time
                zIncrement++;
                
                this.transform.Rotate (_rotationSpeed * Time.deltaTime, 0,  0);
                
                //As we are using physics, we add a forward force to move the cube 
                GetComponent<Rigidbody>().AddForce(Vector3.forward * FBForce);
                //sometimes the cube doen't end at th ecenter of the tile, so we add an offset to hace it
                //propperly placed on the center
                tempPos= new Vector3(currPos.x, currPos.y, zIncrement + zOffset);
                this.transform.position=tempPos;
                //We start a coroutine that helps us to stop moving the cube for
                //a long time
                StartCoroutine(WaitAndResetRotation(resetRotationTime));

                //We do the same on the other directions

            } 
            //Left movement
            if ((Input.GetKeyUp(KeyCode.A) || (Input.GetKeyUp(KeyCode.JoystickButton15))) && currPos.x > -6.9f && canRotate) {
                if(!isMuted) audio.PlayOneShot(LeftSound, 1.0f);
                canRotate=false;
                this.transform.Rotate ( 0,  0, - _rotationSpeed * Time.deltaTime);

                GetComponent<Rigidbody>().AddForce(Vector3.left * LRForce);
                tempPos= new Vector3(xIncrement - xOffset, currPos.y, currPos.z );
                this.transform.position=tempPos;
                xIncrement--;
                StartCoroutine(WaitAndResetRotation(resetRotationTime));
                
                

            } 
            //Back movement
            if ((Input.GetKeyUp(KeyCode.S) || (Input.GetKeyUp(KeyCode.JoystickButton14))) && currPos.z > -4.9f && canRotate) {
                if(!isMuted) audio.PlayOneShot(BackSound, 1.0f);
                canRotate=false;
                zIncrement--;
                
                
                this.transform.Rotate (-_rotationSpeed * Time.deltaTime, 0,  0);
                
                
                GetComponent<Rigidbody>().AddForce(Vector3.back * FBForce);
                tempPos= new Vector3(currPos.x, currPos.y, currPos.z - zOffset);
                this.transform.position=tempPos;
                StartCoroutine(WaitAndResetRotation(resetRotationTime));
                

            } 
            //Right movement
            if ((Input.GetKeyUp(KeyCode.D) || (Input.GetKeyUp(KeyCode.JoystickButton13)))&& currPos.x < 6.9f && canRotate) {
                if(!isMuted) audio.PlayOneShot(RightSound, 1.0f);
                canRotate=false;                        
                this.transform.Rotate ( 0,  0, _rotationSpeed * Time.deltaTime);
                
                GetComponent<Rigidbody>().AddForce(Vector3.right * LRForce);
                tempPos= new Vector3(xIncrement + xOffset, currPos.y, currPos.z );
                this.transform.position=tempPos;
                xIncrement++;
                StartCoroutine(WaitAndResetRotation(resetRotationTime));
                
                
            }

        }
    }

    //We restart the position and orientation of the cube and all the variables that are needed
    public void RestartVariables(){
        tempPos= new Vector3(0, 0, 0);
        this.transform.position=tempPos;
        zIncrement=-1.0f;
        xIncrement=0f;
        Zpos=0.0f;
        this.transform.rotation = Quaternion.identity;
        winnerScene.SetActive(false);
        
    }

    void Mute(){
        isMuted=!isMuted;
        //If it is muted, we show or not a canvas as feedback to the user
        if(isMuted) MuteCanvas.SetActive(true);
        else MuteCanvas.SetActive(false);
    }

    void OnCollisionEnter(Collision collision){
        //If the cube touches to "Goal" tile, he has won and the game finishes.
        if (collision.gameObject.tag == "Goal"){
            //If the user wons we play a sopund to let them know that he has won.
            if(!soundPlayed){
                if(!isMuted) audio.PlayOneShot(WinnerSound, 0.4f);
                soundPlayed=true;
            }
            //We change the color of the goal tile to its original color when the user has reach it
            collision.gameObject.GetComponent<Renderer>().material.SetColor("_Color", ReachTile.colorTile);
            //We show the winner canvas
            winnerScene.SetActive(true);
            won=true;
    
        }
    
    }

    //This coroutine help us to stop the rotation movement and activate it it again to let the user rotate the cube
    //To the next direction
    private IEnumerator WaitAndResetRotation(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        this.transform.rotation = Quaternion.identity;
        canRotate=true;
    }

    public void Restart(){
        //To restart the app, we reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

    


    
}
