using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesByGeneration : MonoBehaviour
{
    public int gridWidth = 10;
    public int gridHeight = 10;

    public GameObject tileTemplate;

    public Dictionary<GameObject, Node> tilesToNode = new Dictionary<GameObject, Node>();

    // Use this for initialization
    void Start()
    {
        Dictionary<Vector3, Node> nodesByPosition = new Dictionary<Vector3, Node>();

        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                GameObject newTile = GameObject.Instantiate(tileTemplate);
                newTile.transform.position = new Vector3(x, 0, z);

                Node tileNode = new Node(newTile.transform.position);
                nodesByPosition.Add(tileNode.position, tileNode);

                //newTile.GetComponent<NodeBinding>().node = tileNode;
                tilesToNode.Add(newTile, tileNode);

                newTile.GetComponent<ReportIfClicked>().generatedTiles = this;
            }
        }

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
