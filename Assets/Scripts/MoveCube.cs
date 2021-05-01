using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MoveCube : MonoBehaviour
{
    
    public float minRotation = -45f;
    public float maxRotation = 45f;
    public int _rotationSpeed = 15;

    public float FBForce = 250.0f;
    public float LRForce = 200.0f;

    public float _waitTime= 0.1f;

    public float YposIncrement = 0.1f; 

    float speedPerSec;
    float speed;
    float zIncrement;
    float xIncrement;
    Vector3 tempPos;

    private float Zpos;

    public float xOffset=0.25f; 
    public float zOffset=0.1f; 

   
    public float resetRotationTime;

    private bool canRotate;
    Vector3 oldPosition;
    private bool won;
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
        if(!PauseManager.isPaused){
        
            if (Input.GetKeyUp(KeyCode.R) || (Input.GetKeyUp(KeyCode.JoystickButton0))){
                RestartVariables();  
            }

            if(won){
                        
                if((Input.GetKeyUp(KeyCode.JoystickButton0))) Restart();

            }

            if((Input.GetKeyUp(KeyCode.JoystickButton6))){
                Mute();
            }

            
            Vector3 currPos = this.transform.position;

            speedPerSec = Vector3.Distance (oldPosition, this.transform.position) / Time.deltaTime;
            speed = Vector3.Distance (oldPosition, this.transform.position);
            oldPosition = this.transform.position;

            Zpos=(float)System.Math.Floor(this.transform.position.z);

            
            if ((Input.GetKeyUp(KeyCode.W ) || (Input.GetKeyUp(KeyCode.JoystickButton12))) && currPos.z < 5.9f && canRotate) {
                if(!isMuted) audio.PlayOneShot(ForwardSound, 1.0f);
                canRotate=false;
                zIncrement++;
                
                this.transform.Rotate (_rotationSpeed * Time.deltaTime, 0,  0);
                
                GetComponent<Rigidbody>().AddForce(Vector3.forward * FBForce);
                tempPos= new Vector3(currPos.x, currPos.y, zIncrement + zOffset);
                this.transform.position=tempPos;
                StartCoroutine(WaitAndResetRotation(resetRotationTime));

            } 
            
            if ((Input.GetKeyUp(KeyCode.A) || (Input.GetKeyUp(KeyCode.JoystickButton15))) && currPos.x > -6.9f && canRotate) {
                if(!isMuted) audio.PlayOneShot(LeftSound, 1.0f);
                canRotate=false;
                this.transform.Rotate ( 0,  0, - _rotationSpeed * Time.deltaTime);

                GetComponent<Rigidbody>().AddForce(Vector3.left * LRForce);
                tempPos= new Vector3(xIncrement - xOffset, currPos.y, currPos.z );
                this.transform.position=tempPos;
                xIncrement--;
                StartCoroutine(WaitAndResetRotation(resetRotationTime));
                
                

            } if ((Input.GetKeyUp(KeyCode.S) || (Input.GetKeyUp(KeyCode.JoystickButton14))) && currPos.z > -4.9f && canRotate) {
                if(!isMuted) audio.PlayOneShot(BackSound, 1.0f);
                canRotate=false;
                zIncrement--;
                
                
                this.transform.Rotate (-_rotationSpeed * Time.deltaTime, 0,  0);
                
                
                GetComponent<Rigidbody>().AddForce(Vector3.back * FBForce);
                tempPos= new Vector3(currPos.x, currPos.y, currPos.z - zOffset);
                this.transform.position=tempPos;
                StartCoroutine(WaitAndResetRotation(resetRotationTime));
                

            } if ((Input.GetKeyUp(KeyCode.D) || (Input.GetKeyUp(KeyCode.JoystickButton13)))&& currPos.x < 6.9f && canRotate) {
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

    public void RestartVariables(){
        tempPos= new Vector3(0, 0, 0);
        this.transform.position=tempPos;
        //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        zIncrement=-1.0f;
        xIncrement=0f;
        Zpos=0.0f;
        this.transform.rotation = Quaternion.identity;
        winnerScene.SetActive(false);
        
    }

    void Mute(){
        isMuted=!isMuted;
        if(isMuted) MuteCanvas.SetActive(true);
        else MuteCanvas.SetActive(false);
    }

    void OnCollisionEnter(Collision collision){
        if (collision.gameObject.tag == "Goal"){
            if(!soundPlayed){
                if(!isMuted) audio.PlayOneShot(WinnerSound, 0.4f);
                soundPlayed=true;
            }
            collision.gameObject.GetComponent<Renderer>().material.SetColor("_Color", ReachTile.colorTile);
            Debug.Log("GoalReached");
            winnerScene.SetActive(true);
            won=true;
    
        }
    
    }

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
