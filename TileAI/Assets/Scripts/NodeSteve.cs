using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSteve
{
    public NodeSteve(Vector3 pos)
    {
        this.position = pos;
    }
    public Vector3 position;
    public Dictionary<Node, float> weightedConnections = new Dictionary<Node, float>();
}
