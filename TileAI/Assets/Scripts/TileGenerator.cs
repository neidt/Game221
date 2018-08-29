using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public int gridHeight = 5;
    public int gridWidth = 5;
    public GameObject tile;
    Quaternion tileRotation;
    Vector3 startSpot;


    // Use this for initialization
    void Start()
    {
        tile = GameObject.FindGameObjectWithTag("Tile");
        tileRotation = new Quaternion(0, 0, 0, 0);
    }

    //awake stuff?? make stuff???
    public void Awake()
    {
        //generate graph
        for (int z = 0; z < gridHeight; z++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                Instantiate(tile, new Vector3(x, 2, z),tileRotation);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
