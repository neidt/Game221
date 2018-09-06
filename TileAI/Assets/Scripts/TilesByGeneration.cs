using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesByGeneration : MonoBehaviour
{
    public int gridWidth = 5;
    public int gridHeight = 5;

    //tiles
    public GameObject tileTemplate;
    //public GameObject tileTemplate;
    public GameObject obstacleTile;

    //other stuff
    public GameObject player;
    public Dictionary<GameObject, Node> tilesToNode = new Dictionary<GameObject, Node>();
    public List<GameObject> tiles = new List<GameObject>();
    public List<Node> nodesList = new List<Node>();
    public Dictionary<Node, float> weightedConnections = new Dictionary<Node, float>();
    public Dictionary<Vector3, Node> nodesByPosition = new Dictionary<Vector3, Node>();

    ClickToMoveAI playerMoveScript;

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

                Node tileNode = new Node(newTile.transform.position);
                if (z == 0 && x == 0)
                {
                    playerMoveScript.AIStartNode = tileNode;
                }
                //print("Making node at: " + tileNode.position.ToString());
                nodesByPosition.Add(tileNode.position, tileNode);
                tilesToNode.Add(newTile, tileNode);

                tiles.Add(newTile);
                nodesList.Add(tileNode);

                newTile.GetComponent<ReportIfClicked>().generatedTiles = this;
            }
        }

        //connections
        foreach (Vector3 nodePosition in nodesByPosition.Keys)
        {
            //Debug.Log("tile gen nodesBypos: " + nodesByPosition.Count);
            Node currentNode = nodesByPosition[nodePosition];
            weightedConnections = currentNode.weightedConnections;
            //Debug.Log("weighted conns: " + weightedConnections.Count);

            Node right = LookupNode(nodesByPosition, currentNode.position + Vector3.right);
            if (right != null)
            {
                weightedConnections.Add(right, 1);
            }
            else
            {
                Debug.Log("right node is null!");
            }

            Node left = LookupNode(nodesByPosition, currentNode.position + Vector3.left);
            if (left != null)
            {
                weightedConnections.Add(left, 1);
            }
            else
            {
                Debug.Log("left node is null!");
            }

            Node up = LookupNode(nodesByPosition, currentNode.position + Vector3.forward);
            if (up != null)
            {
                weightedConnections.Add(up, 1);
            }
            else
            {
                Debug.Log("up node is null!");
            }

            Node down = LookupNode(nodesByPosition, currentNode.position + Vector3.back);
            if (down != null)
            {
                weightedConnections.Add(down, 1);
            }
            else
            {
                Debug.Log("down node is null!");
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
