using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code helps the camera to follow the cube on the scene

public class FollowCube : MonoBehaviour
{
    public Transform cube;
    public float offset;
    
    void Update()
    {
        //We take the position of the cube and we add an offset to have it behind the cube
        this.transform.position=new Vector3(cube.transform.position.x, this.transform.position.y, cube.transform.position.z - offset);
    }
}
