using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public int gridHeight = 5;
    public int gridWidth = 5;
    public GameObject tile;
    public GameObject player;
    Vector3 startSpot;


    // Use this for initialization
    void Start()
    {
        tile = GameObject.FindGameObjectWithTag("Tile");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //awake stuff?? make stuff???
    public void Awake()
    {
        //generate player
        GameObject playerObj = Instantiate(player, new Vector3(0, 0, 0),this.transform.rotation);
        //generate graph
        for (int z = 0; z < gridHeight; z++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                GameObject tileobj = Instantiate(tile, new Vector3(x, 0, z), this.transform.rotation);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
