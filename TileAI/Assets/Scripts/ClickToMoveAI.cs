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
    private bool hitThisFrame = false;
    private Vector3 hitLocThisFrame = Vector3.zero;

    //other stuff
    ReportIfClicked clickedScript;

    //pathfinding stuffs
    //public List<Vector3> clickedList = new List<Vector3>();
    public List<Vector3> waypointList;
    public Node AIStartNode;
    public Node endNode;
    public int waypointNumber = 0;

    // Use this for initialization
    void Start()
    {
        clickedScript = GameObject.FindGameObjectWithTag("Tile").GetComponent<ReportIfClicked>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           DoDijkstra();
        } 
        UpdateAgentMovement();
        
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            ResetWaypoints();
        }
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
                    AIStartNode.position = playerPos;
                }
            }
        }
    }

    private void DoDijkstra()
    {
        //AIStartNode.position = playerPos;
        print("ai start node: " + AIStartNode);
        //print("ai connections" + AIStartNode.weightedConnections.Count);
        print("endnode: " + endNode);
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
