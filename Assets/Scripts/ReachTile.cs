using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This code help us to choose randomly the goal tile that the cube must reach 

public class ReachTile : MonoBehaviour
{
    
    private GameObject[] tiles;
    private int index;
    public static Color colorTile;
    void Start()
    {
        //We created a tag for all the tiles on the board and we saved them into an array
        tiles = GameObject.FindGameObjectsWithTag("Tile");
        //We create a random number and we color the tile to show the user the goal tile
        index= (int) Random.Range(0, tiles.Length);
        colorTile=tiles[index].GetComponent<Renderer>().material.GetColor("_Color");
        tiles[index].GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        //We assigned this tile the "Goal" tag
        tiles[index].tag = "Goal";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
