using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportIfClicked : MonoBehaviour
{
    public TilesByGeneration generatedTiles;
    ClickToMoveAI ai;
    // TilesByGeneration genScript;
    public Node myEndNode;

    //public GameObject endTile;
    public List<Vector3> waypointList;

    // Use this for initialization
    void Start()
    {
        ai = GameObject.FindGameObjectWithTag("Player").GetComponent<ClickToMoveAI>();
        //genScript = GameObject.FindGameObjectWithTag("Generator").GetComponent<TilesByGeneration>();
        //myNodeGameObj = this.gameObject;
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
                    print("ray hit at" + info.transform.position.ToString());
                    //ai.endNode = this.gameObject.GetComponent<Node>();

                    //set node at the transform to the end node???
                    //myEndNode = generatedTiles.tilesToNode[endTile];
                    //generatedTiles.tilesToNode.Add(this.gameObject,myEndNode);


                    print("setting end node to: " + ai.endNode.position);

                    //waypointList = DijkstraImplementation.Pathfind(ai.AIStartNode, myEndNode);
                }
            }
        }


        /*
        if (Input.GetKey(KeyCode.Space) && waypointList.Count > 0)
        {
            //waypointList = DijkstraImplementation.Pathfind(ai.AIStartNode, ai.endNode);
            foreach (Vector3 waypointThing in waypointList)
            {
                print("moving to node: " + waypointList[0].ToString());
                transform.position = Vector3.MoveTowards(ai.transform.position, waypointList[0], .1f);
                if (Vector3.Distance(ai.transform.position, waypointList[0]) < .3f)
                {
                    waypointList.Remove(waypointList[0]);
                }
            }
        }*/
    }
}

