using System;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Dictionary<Node, int> connectionsList = new Dictionary<Node, int>();
    public GameObject tile;

    public void DisplayConnectionList()
    {
        foreach (KeyValuePair<Node,int> connection in connectionsList)
        {
            Debug.Log("Tile: " + connection.Key.tile.transform.name +
                "Cost: " + connection.Value);
        }
    }
}

