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
    private Vector3 rayCollisionNormal;
    private Vector3 hitLocThisFrame = Vector3.zero;

    //pathfinding stuffs
    public List<Vector3> clickedList = new List<Vector3>();

    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        hitThisFrame = false;
        float step = moveSpeed * Time.deltaTime;
        Ray intoScreen = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        playerPos = playerModel.transform.position;
        //add mouse click spot to list of spots to travel along
        if (Input.GetMouseButtonDown(0))
        {
            //Ray intoScreen = Camera.main.ScreenPointToRay(Input.mousePosition); // hits whatever is under mouse at the time 
            if (Physics.Raycast(intoScreen, out hitInfo, 1000, floorOnly))
            {
                print("raycast hit " + hitInfo.transform.name + " at " + hitInfo.point);
                if (hitInfo.transform.tag == "Tile")
                {
                    destination.x = hitInfo.transform.position.x;
                    destination.z = hitInfo.transform.position.z;
                    clickedList.Add(destination);
                }
            }
        }
        
        //dijkstra stuff??
        if (Input.GetMouseButtonDown(1))
        {
            if(Physics.Raycast(intoScreen, out hitInfo, 1000, floorOnly))
            {
                Vector3 endNode = hitInfo.point;
                print("final spot: " + endNode);
            }
        }

        //move along path
        if (clickedList.Count > 0 && Input.GetKey(KeyCode.Space))
        {
            transform.position = Vector3.MoveTowards(playerPos, clickedList[0], step);
            if (Vector3.Distance(playerPos, clickedList[0]) < rayDistance)
            {
                clickedList.Remove(clickedList[0]);
            }
        }

    }

    public void DijkstraTest1(Vector3 startNode, Vector3 endNode)
    {

    }
    
}
