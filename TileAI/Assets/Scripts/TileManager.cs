using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    //public Transform moveLoc;
    public ClickToMoveAI AI;
    public Node tileNode = new Node();
    public int graphsize;
    // Use this for initialization
    void Start()
    {
        //AI = GameObject.FindGameObjectWithTag("Player").GetComponent<ClickToMoveAI>();
    }
    
    // Update is called once per frame
    void Update()
    {

    }

    /*
    public void SendPosToAI()
    {
        AI.clickedList = DijkstraImplementation.Pathfind(AI.startNode,this.tileNode);
    }*/
}
