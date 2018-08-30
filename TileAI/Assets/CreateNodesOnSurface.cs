using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNodesOnSurface : MonoBehaviour
{
    public float tileSize = 1.0f;
    public GameObject testSphere;

	// Use this for initialization
	void Start ()
    {

        Dictionary<Vector3, Node> nodesByPosition = new Dictionary<Vector3, Node>();
        int gridWidth = Mathf.RoundToInt(transform.localScale.x);
        int gridHeight = Mathf.RoundToInt(transform.localScale.y);
        
        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {
                GameObject sphereNode = GameObject.Instantiate(testSphere);
                Vector3 nodePosition = new Vector3(x, transform.position.y+1, z);
                
                Node tileNode = new Node(nodePosition);
                nodesByPosition.Add(tileNode.position, tileNode);
                
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		

	}

    Node LookupNode(Dictionary<Vector3, Node> nodes, Vector3 lookup)
    {
        if (!nodes.ContainsKey(lookup))
        {
            return null;
        }
      
        return nodes[lookup];
    }

}
