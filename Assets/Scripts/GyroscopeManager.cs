using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code implements the Gyroscope tp the camera to be able to use VR in the game

public class GyroscopeManager : MonoBehaviour
{
    private bool gyroscopeEnabled;
    private Gyroscope gyro;

    private GameObject cameraContainer;
    private Quaternion rot;

    // Start is called before the first frame update
    void Start()
    {
        //We creater an empty object that has our camera to be able to move it easier
        cameraContainer = new GameObject ("CameraContainer");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);

        gyroscopeEnabled = EnableGyroscope();
    }

    // Update is called once per frame
    void Update()
    {
        //If the gyroscope is enabled, then we can follow the rotation of the world
        if(gyroscopeEnabled){
            transform.localRotation = gyro.attitude * rot;
        }
        
    }

    //This function helps us to know if the device has a gyroscope that we can use.
    //It heps us to avoid some errors it he device doesn't has a gyroscope
    private bool EnableGyroscope(){
        if(SystemInfo.supportsGyroscope){
            gyro = Input.gyro;
            gyro.enabled=true;

            cameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 0f);
            rot=new Quaternion(0,0,1,0);
            
            return true;
        }
        return false;

    }
}
