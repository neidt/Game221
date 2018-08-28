using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public int graphSize;
    public GameObject tile;
    public GameObject obstacle;
    Vector3 startSpot;


    // Use this for initialization
    void Start ()
    {
        tile = GameObject.FindGameObjectWithTag("Tile");
	}

    //awake stuff?? make stuff???
    public void Awake()
    {
        //generate graph
        for (int i = 0; i < graphSize; i++)
        {
            //make the grid
        }
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
