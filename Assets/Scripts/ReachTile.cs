using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachTile : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] tiles;
    private int index;
    public static Color colorTile;
    void Start()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
        index= (int) Random.Range(0, tiles.Length);
        colorTile=tiles[index].GetComponent<Renderer>().material.GetColor("_Color");
        tiles[index].GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        tiles[index].tag = "Goal";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
