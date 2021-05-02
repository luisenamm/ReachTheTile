﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeManager : MonoBehaviour
{
    private bool gyroscopeEnabled;
    private Gyroscope gyro;

    private GameObject cameraContainer;
    private Quaternion rot;

    // Start is called before the first frame update
    void Start()
    {
        cameraContainer = new GameObject ("CameraContainer");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);

        gyroscopeEnabled = EnableGyroscope();
    }

    // Update is called once per frame
    void Update()
    {
        if(gyroscopeEnabled){
            transform.localRotation = gyro.attitude * rot;
        }
        
    }

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
