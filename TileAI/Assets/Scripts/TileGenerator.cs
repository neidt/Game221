using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public int gridHeight = 5;
    public int gridWidth = 5;
    public GameObject tile;
    public GameObject player;
    Vector3 startSpot;
    List<Node> nodeList = new List<Node>();
    ClickToMoveAI AI;

    // Use this for initialization
    void Start()
    {
        tile = GameObject.FindGameObjectWithTag("Tile");
        player = GameObject.FindGameObjectWithTag("Player");
        AI = GameObject.FindGameObjectWithTag("Player").GetComponent<ClickToMoveAI>();

    }

    //awake stuff?? make stuff???
    public void Awake()
    {
        //generate player
        GameObject playerObj = Instantiate(player, new Vector3(0, 0, 0), this.transform.rotation);
        //generate graph
        for (int z = 0; z < gridHeight; z++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                GameObject tileobj = Instantiate(tile, new Vector3(x, 0, z), this.transform.rotation);

                //Node tileNode = new Node();
                Node tileNode = tileobj.GetComponent<TileManager>().tileNode;
                //tileobj.GetComponent<TileManager>().tileNode = tileNode;
                tileNode.position = tileobj.transform.position;
                nodeList.Add(tileNode);
            }
        }

        foreach (Node tileNode in nodeList)
        {
            Vector3 tileNodePos = tileNode.position;
            foreach (Node node in nodeList)
            {
                if (tileNodePos == node.position + Vector3.up ||
                tileNodePos == node.position + Vector3.down ||
                tileNodePos == node.position + Vector3.right ||
                tileNodePos == node.position + Vector3.left)
                {
                    tileNode.connections.Add(node, 1);
                }
            }
        }
    }

    public void SendPosToAI()
    {
        nodeList[0] = AI.startNode;
        //AI.clickedList = DijkstraImplementation.Pathfind(AI.startNode,AI.endNode);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
