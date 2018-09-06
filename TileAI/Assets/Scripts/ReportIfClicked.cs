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
                    //print("ray hit at" + info.transform.position.ToString());
                    //ai.endNode = this.gameObject.GetComponent<Node>();
                    //set node at the transform to the end node??
                    endTile = this.gameObject;
                    ai.endNode = generatedTiles.tilesToNode[endTile];

                    print("setting end node to: " + ai.endNode.position);
                    //waypointList = DijkstraImplementation.Pathfind(ai.AIStartNode, myEndNode);
                }
            }
        }



    }
}

