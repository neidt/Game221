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
    public float radiusOfSat = .3f;
    //in case we hit
    private bool hitThisFrame = false;
    private Vector3 rayCollisionNormal;
    private Vector3 hitLocThisFrame = Vector3.zero;


    //grid stuff
    int rows = 3;
    int cols = 3;
    Transform[,] grid;
    
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo;
        hitThisFrame = false;
        float step = moveSpeed * Time.deltaTime;
        Vector3 playerPos = playerModel.transform.position;

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
                //rayCollisionNormal = hitInfo.normal;
            }
        }
       

        //move the player toward the destination
        if (Vector3.Distance(playerPos, destination) > rayDistance)
        {
            transform.position = Vector3.MoveTowards(playerModel.transform.position, destination, step);
            /*if (Vector3.Distance(playerPos, destination) > radiusOfSat)
            {
                //if playerpos.x  is same as destination.x, and z is different
                if (playerPos.x == destination.x && playerPos.z != destination.z)
                {
                    //if des.z is > playerpos.z, move up
                    if (destination.z > playerPos.z)
                    {
                        playerPos.z += .1f;
                    }
                    //if des.z is < playerpos.z, move down
                    if (destination.z < playerPos.z)
                    {
                        playerPos.z -= .1f;
                    }
                }

                //if playerpos.z is same as destination.z and x is different
                if (playerPos.z == destination.z && playerPos.x != destination.x)
                {
                    //if des.x is > playerpos.x, move right
                    if (destination.x > playerPos.x)
                    {
                        playerPos.x += .1f;
                    }
                    //if des.x is < playerpos.x, move left
                    if (destination.x < playerPos.x)
                    {
                        playerPos.x -= .1f;
                    }
                }
            }*/
        }
    }
}
