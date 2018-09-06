﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToMoveAI : MonoBehaviour
{
    public Transform playerModel;
    public float moveSpeed = 7.0f;
    public LayerMask raycastLayers;
    public LayerMask floorOnly;
    private Vector3 destination;
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
    public Node AIStartNode = new Node(Vector3.zero);
    public Node endNode;

    // Use this for initialization
    void Start()
    {
        clickedScript = GameObject.FindGameObjectWithTag("Tile").GetComponent<ReportIfClicked>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            DoDijkstra();
            UpdateAgentMovement();
        }

       

    }

    private void UpdateAgentMovement()
    {
        float step = moveSpeed * Time.deltaTime;
        //RaycastHit hitInfo;
        playerPos = playerModel.transform.position;

        foreach (Vector3 waypointThing in waypointList)
        {
            print("moving to node: " + waypointThing.ToString());
            destination.x = waypointThing.x;
            destination.z = waypointThing.z;

            transform.position = Vector3.MoveTowards(playerPos, destination, step);
            if (Vector3.Distance(playerPos, waypointThing) < rayDistance)
            {
                //move to next node
            }
        }
    }

    private void DoDijkstra()
    {
        print("ai start node: " + AIStartNode);
        print("ai connections" + AIStartNode.weightedConnections.Count);
        print("endnode: " + endNode);
        waypointList = DijkstraImplementation.Pathfind(AIStartNode, endNode);
        waypointList.ToString();
        foreach (Vector3 waypoint in waypointList)
        {
            print("waypoint at: " + waypoint.ToString());
        }
    }
}
