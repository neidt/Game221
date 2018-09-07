using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportIfClicked : MonoBehaviour
{
    public TilesByGeneration generatedTiles;
    ClickToMoveAI ai;
    public GameObject endTile;

    // Use this for initialization
    void Start()
    {
        ai = GameObject.FindGameObjectWithTag("Player").GetComponent<ClickToMoveAI>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit info;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out info))
            {
                if (info.transform == this.transform)
                {
                    //set node at the transform to the end node??
                    endTile = this.gameObject;
                    ai.endNode = generatedTiles.tilesToNode[endTile];

                    print("setting end node to: " + ai.endNode.position);
                }
            }
        }
        //if rightclicking, set object to an obstacle??
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit info;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out info))
            {
                if (info.transform == this.transform)
                {
                    //disable the current object and enable an obstacle object in its place
                    //also remove the area from the node list, as it isn't connected anymore
                    generatedTiles.obstacleTile.transform.position = info.transform.position;
                    Instantiate(generatedTiles.obstacleTile, info.transform);
                    generatedTiles.tiles.Remove(this.gameObject);
                    generatedTiles.tilesToNode.Remove(this.gameObject);
                    

                }
            }
        }
    }
}

