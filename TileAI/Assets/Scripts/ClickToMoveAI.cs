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
    public float rayDistance = .1f;
    //in case we hit
    private bool hitThisFrame = false;
    private Vector3 rayCollisionNormal;
    private Vector3 hitLocThisFrame = Vector3.zero;


    //grid stuff
    int rows = 3;
    int cols = 3;
    Transform[,] grid;
    //public GameObject[] TilesList;  


    // Use this for initialization
    void Start()
    {
        //destination = playerModel.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo;
        hitThisFrame = false;
        float step = moveSpeed * Time.deltaTime;
        
        //move based on mouse click
        if (Input.GetMouseButtonDown(0))
        {
            Ray intoScreen = Camera.main.ScreenPointToRay(Input.mousePosition); // hits whatever is under mouse at the time 
            if (Physics.Raycast(intoScreen, out hitInfo, 1000, floorOnly))
            {
                print("raycast hit " + hitInfo.transform.name + " at " + hitInfo.point);
                //destination = hitInfo.point;
                if (hitInfo.transform.tag == "Tile")
                {
                    destination.x = hitInfo.transform.position.x;
                    destination.z = hitInfo.transform.position.z;
                }
                //playerModel.LookAt(destination);

                //rayCollisionNormal = hitInfo.normal;
            }
        }
        Vector3 playerPos = playerModel.transform.position;
        //move the player toward the destination
        if (Vector3.Distance(playerPos, destination) > rayDistance)
        {
            //if playerpos.x  is same as destination.x, and z is different
                //if des.z is > playerpos.z, mo
            playerPos = Vector3.MoveTowards(playerPos, destination, step);

        }
    }
}
