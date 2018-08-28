using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public Node tileNode = new Node();
    public Transform moveLoc;
    public ClickToMoveAI AI;

	// Use this for initialization
	void Start ()
    {
        AI = GameObject.FindGameObjectWithTag("Player").GetComponent<ClickToMoveAI>();
        tileNode.tile = this.gameObject;
	}

    public void Awake()
    {
        foreach(GameObject tile in GameObject.FindGameObjectsWithTag("Tile"))
        {
            Vector3 tilePos = tile.transform.position;
            if (tilePos == this.transform.position + Vector3.up ||
                tilePos == this.transform.position + Vector3.down ||
                tilePos == this.transform.position + Vector3.right ||
                tilePos == this.transform.position + Vector3.left)
            {
                tileNode.connectionsList.Add(tile.GetComponent<TileManager>().tileNode, 1);
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
    public void SendPosToAI()
    {
        //ai.addpoint(moveloc.psotioin
        //node.dispalyconnection();
    }
}
