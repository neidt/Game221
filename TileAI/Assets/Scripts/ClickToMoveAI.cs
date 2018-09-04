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
    public Vector3 endPos = new Vector3(4f, 0f, 4f);


    ReportIfClicked clickedScript;
    public TilesByGeneration generatorThing;
    public DijkstraImplementation dijkstra;

    //in case we hit
    private bool hitThisFrame = false;
    private Vector3 hitLocThisFrame = Vector3.zero;

    //pathfinding stuffs
    //public List<Vector3> clickedList = new List<Vector3>();
    public List<Vector3> waypointList;
    public List<Vector3> clickedList;
    public Node AIStartNode = new Node(Vector3.zero);
    public Node endNode;


    // Use this for initialization
    void Start()
    {
        clickedScript = GameObject.FindGameObjectWithTag("Tile").GetComponent<ReportIfClicked>();
        if (endNode == null)
        {
            print("end node is null.");
            endNode = clickedScript.myEndNode;
            print("set endnode  to: " + endNode);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        float step = moveSpeed * Time.deltaTime;
        RaycastHit hitInfo;
        playerPos = playerModel.transform.position;

        //add mouse click spot to list of spots to travel along
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 1000, floorOnly))
            {
                //print("raycast hit " + hitInfo.transform.name + " at " + hitInfo.point);
                if (hitInfo.transform.tag == "Tile")
                {
                    destination.x = hitInfo.transform.position.x;
                    destination.z = hitInfo.transform.position.z;

                    //create new node and add it to the list of nodes somehow?
                    //or modify the list at that point to include the node?
                    //endNode = clickedScript.myEndNode;
                    //if the node at hitinfo.point isn't null (meaning there is a node already there)
                    //change the nodes key to this objects key?
                    //print("waypoints: " + waypointList.ToString());
                }
            }
        }

        /*
        if (Input.GetKey(KeyCode.Space) && waypointList.Count > 0)
        {
            //waypointList = DijkstraImplementation.Pathfind(AIStartNode, endNode);
            foreach (Vector3 waypointThing in waypointList)
            {
                print("moving to node: " + waypointList[0].ToString());
                transform.position = Vector3.MoveTowards(playerPos, waypointList[0], step);
                if (Vector3.Distance(playerPos, waypointList[0]) < rayDistance)
                {
                    waypointList.Remove(waypointList[0]);
                }
            }
        }*/
    }
}
