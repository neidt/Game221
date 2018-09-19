using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToMoveAI : MonoBehaviour
{
    public Transform playerModel;
    public float moveSpeed = 7.0f;
    public LayerMask raycastLayers;
    public LayerMask floorOnly;
    Vector3 playerPos;
    public float rayDistance = .1f;

    //in case we hit
    //private bool hitThisFrame = false;
    //private Vector3 hitLocThisFrame = Vector3.zero;

    //other stuff
    //ReportIfClicked clickedScript;
    TilesByGeneration tileGen;

    //pathfinding stuffs
    public List<Vector3> waypointList;
    public Node AIStartNode;
    public Node endNode;
    public int waypointNumber = 0;
    public bool userInputDetected;
    public bool waypointListHasChanged;

    // Use this for initialization
    void Start()
    {
        tileGen = GameObject.FindGameObjectWithTag("Generator").GetComponent<TilesByGeneration>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && ChooseInput.isMouseEnabled == true)
        {
            RaycastHit info;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out info))
            {
                //goal: hitsomething->what did u hit,is it a tile, if yes, do movement stuff
                //info's tag is tile means you hit a tile, use tilesToNode to get node
                //set that node to the destination node and do the a* using that node
                //do a* in here
                if (info.transform.tag == "Tile")
                {
                    Debug.Log("setting end point");
                    endNode = tileGen.tilesToNode[info.transform.gameObject];
                    print("Endpoint set, doing a*");
                    DoAStar();
                }

                //clicking should make agent waypoints update and do continuous movement
                //if the list of waypoints changes, redo a* pathfinding
                if (waypointListHasChanged == true)
                {
                    print("waypoints changed, redoing a*");
                    DoAStar();
                }
            }
        }

        UpdateAgentMovement();
    }

    private void UpdateAgentMovement()
    {
        float step = moveSpeed * Time.deltaTime;
        playerPos = playerModel.transform.position;
        if (waypointList.Count != 0)
        {
            transform.position = Vector3.MoveTowards(playerPos, waypointList[waypointNumber], step);
            if (transform.position == waypointList[waypointNumber])
            {
                waypointNumber++;
                if (waypointNumber == waypointList.Count)
                {
                    ResetWaypoints();
                    
                    AIStartNode = tileGen.nodesByPosition[playerPos];
                }
            }
        }
    }

    private void DoAStar()
    {
        //AIStartNode.position = playerPos;
        //print("ai start node: " + AIStartNode);
        print("ai connections" + AIStartNode.weightedConnections.Count);
        print("endnode connections: " + endNode.weightedConnections.Count);
        //print("endnode: " + endNode);
        print("doing a*");
        waypointList = DijkstraImplementation.Pathfind(AIStartNode, endNode);
    }

    private void ResetWaypoints()
    {
        print("resetting");
        waypointNumber = 0;
        AIStartNode = endNode;
        waypointList.Clear();
    }
}
