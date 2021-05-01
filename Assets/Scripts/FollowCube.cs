using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCube : MonoBehaviour
{
    public Transform cube;
    public float offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position=new Vector3(cube.transform.position.x, this.transform.position.y, cube.transform.position.z - offset);
    }
}
