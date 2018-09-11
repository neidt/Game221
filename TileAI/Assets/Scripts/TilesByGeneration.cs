using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesByGeneration : MonoBehaviour
{
    public int gridWidth = 5;
    public int gridHeight = 5;

    //tiles
    public GameObject tileTemplate;
    public GameObject obstacleTile;

    public Material tileMat;
    public Material obstacleMat;


    //other stuff
    public GameObject player;
    public Dictionary<GameObject, Node> tilesToNode = new Dictionary<GameObject, Node>();
    public Dictionary<Node, GameObject> nodesToTile = new Dictionary<Node, GameObject>();
    public List<GameObject> tiles = new List<GameObject>();
    public List<Node> nodesList = new List<Node>();
    public Dictionary<Node, float> weightedConnections = new Dictionary<Node, float>();
    public Dictionary<Vector3, Node> nodesByPosition = new Dictionary<Vector3, Node>();

    ClickToMoveAI playerMoveScript;
    //ReportIfClicked tileScript;

    // Use this for initialization
    void Start()
    {
        Dictionary<Vector3, Node> nodesByPosition = new Dictionary<Vector3, Node>();

        GameObject playerObj = Instantiate(player, new Vector3(0, 0, 0), this.transform.rotation);
        
        playerMoveScript = playerObj.GetComponent<ClickToMoveAI>();

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

                nodesToTile.Add(tileNode, newTile);

                tiles.Add(newTile);
                nodesList.Add(tileNode);

                newTile.GetComponent<ReportIfClicked>().generatedTiles = this;
            }
        }
        //connections
        makeConnections();
    }

    public void makeConnections()
    {
        foreach (Vector3 nodePosition in nodesByPosition.Keys)
        {
            Node currentNode = nodesByPosition[nodePosition];
            weightedConnections.Clear();
            weightedConnections = currentNode.weightedConnections;

            GameObject obj = nodesToTile[currentNode];
            ReportIfClicked tileScript;
            tileScript = obj.GetComponent<ReportIfClicked>();

            Node right = LookupNode(nodesByPosition, currentNode.position + Vector3.right);
            if (right != null)
            {
                //if right isn't an obstacle, add it to the list
                if (tileScript.isAnObstacle == false)
                {
                    weightedConnections.Add(right, 1);
                }
            }

            Node left = LookupNode(nodesByPosition, currentNode.position + Vector3.left);
            if (left != null)
            {
                if (tileScript.isAnObstacle == false)
                {
                    weightedConnections.Add(left, 1);
                }
            }

            Node up = LookupNode(nodesByPosition, currentNode.position + Vector3.forward);
            if (up != null)
            {
                if (tileScript.isAnObstacle == false)
                {
                    weightedConnections.Add(up, 1);
                }
            }

            Node down = LookupNode(nodesByPosition, currentNode.position + Vector3.back);
            if (down != null)
            {
                if (tileScript.isAnObstacle == false)
                {
                    weightedConnections.Add(down, 1);
                }
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
