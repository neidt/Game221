using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesByGeneration : MonoBehaviour
{
    public int gridWidth = 5;
    public int gridHeight = 5;

    public GameObject tileTemplate;
    public GameObject player;
    public Dictionary<GameObject, Node> tilesToNode = new Dictionary<GameObject, Node>();
    //dubuge
    public List<GameObject> tiles = new List<GameObject>();
    public List<Node> nodesList = new List<Node>();
    //end debug

    //public Node node;
    ClickToMoveAI playerMoveScript;
    //public ReportIfClicked clickScript;

    // Use this for initialization
    void Start()
    {
        Dictionary<Vector3, Node> nodesByPosition = new Dictionary<Vector3, Node>();
        GameObject playerObj = Instantiate(player, new Vector3(0, 0, 0), this.transform.rotation);

        playerMoveScript = playerObj.GetComponent<ClickToMoveAI>();
        //clickScript = tileTemplate.GetComponent<ReportIfClicked>();

        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                GameObject newTile = GameObject.Instantiate(tileTemplate);
                newTile.transform.position = new Vector3(x, 0, z);
                //print("Making tile at: " + newTile.transform.position.ToString());

                Node tileNode = new Node(newTile.transform.position);
                //print("Making node at: " + tileNode.position.ToString());
                nodesByPosition.Add(tileNode.position, tileNode);
                tilesToNode.Add(newTile, tileNode);

                tiles.Add(newTile);
                nodesList.Add(tileNode);

                newTile.GetComponent<ReportIfClicked>().generatedTiles = this;
                /*
                if (tileNode.position == Vector3.zero)
                {
                    playerMoveScript.AIStartNode = tileNode;
                }*/
            }
        }

        //debug statements
        foreach (GameObject tile in tiles)
        {
            print("tile: " + tile.transform.position.ToString());
        }

        foreach(Node tileNode in nodesList)
        {
            print("node: " + tileNode.position.ToString());
        }



        //connections
        foreach (Vector3 nodePosition in nodesByPosition.Keys)
        {
            Node currentNode = nodesByPosition[nodePosition];
            Dictionary<Node, float> weightedConnections = currentNode.weightedConnections;

            Node right = LookupNode(nodesByPosition, currentNode.position + Vector3.right);
            if (right != null)
            {
                weightedConnections.Add(right, 1);
            }
            Node left = LookupNode(nodesByPosition, currentNode.position + Vector3.left);
            if (left != null)
            {
                weightedConnections.Add(left, 1);
            }
            Node up = LookupNode(nodesByPosition, currentNode.position + Vector3.up);
            if (up != null)
            {
                weightedConnections.Add(up, 1);
            }
            Node down = LookupNode(nodesByPosition, currentNode.position + Vector3.down);
            if (down != null)
            {
                weightedConnections.Add(down, 1);
            }
        }
    }

    Node LookupNode(Dictionary<Vector3, Node> nodes, Vector3 lookup)
    {
        if (!nodes.ContainsKey(lookup))
        {
            return null;
        }
        return nodes[lookup];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
