using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportIfClicked : MonoBehaviour
{
    public TilesByGeneration generatedTiles;
    ClickToMoveAI ai;
    public GameObject endTile;
    bool mouseOrNah;
    public bool isAnObstacle;

    // Use this for initialization
    void Start()
    {
        ai = GameObject.FindGameObjectWithTag("Player").GetComponent<ClickToMoveAI>();

    }

    // Update is called once per frame
    void Update()
    {
        if (ChooseInput.mouseOrNah == true)
        {
            //if (Input.GetMouseButtonDown(0))
            //{
            //    ai.userInputDetected = true;
            //    RaycastHit info;
            //    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out info))
            //    {
            //        if (info.transform == this.transform)
            //        {
            //            //set node at the transform to the end node??
            //            endTile = this.gameObject;
            //            ai.endNode = generatedTiles.tilesToNode[endTile];

            //            print("setting end node to: " + ai.endNode.position);
            //        }
            //    }
            //}
            //if rightclicking, set object to an obstacle??
            if (Input.GetMouseButtonDown(1))
            {
                RaycastHit info;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out info))
                {
                    if (info.transform == this.transform)
                    {
                        if (!isAnObstacle)
                        {
                            //change to obstacle
                            //get mesh renderer here
                            GetComponent<MeshRenderer>().material = generatedTiles.obstacleMat;
                            isAnObstacle = true;
                            //the waypoint list probably changed, so send that info to the ai
                            ai.waypointListHasChanged = true;
                            generatedTiles.makeConnections();
                        }
                        else
                        {
                            GetComponent<MeshRenderer>().material = generatedTiles.tileMat;
                            ai.waypointListHasChanged = true;
                            isAnObstacle = false;
                            generatedTiles.makeConnections();
                        }
                        
                    }
                }
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                //move selected tile to the one above it
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                //move selector down, set current tile
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                //move selector right,set current tile
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //move selector left, set current tile
            }
        }
    }
}

