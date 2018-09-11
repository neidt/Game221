using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DijkstraImplementation
{
    public static List<Vector3> Pathfind(Node fromNode, Node toNode)
    {
        //Debug.Log("from node: " + fromNode.weightedConnections.Count);
        //Debug.Log("tonode: " + toNode.weightedConnections.Count);
        List<Vector3> waypoints = new List<Vector3>();

        List<PathFindingNode> openList = new List<PathFindingNode>();
        List<PathFindingNode> closedList = new List<PathFindingNode>();

        Dictionary<Node, PathFindingNode> pathfindingNodes = new Dictionary<Node, PathFindingNode>();

        pathfindingNodes.Add(fromNode, new PathFindingNode(fromNode));
        openList.Add(pathfindingNodes[fromNode]);

        while (openList.Count > 0 && !DoesListContainNode(toNode, closedList))
        {
            //Debug.Log("into while loop");
            //how do you know what the smallest cost-so-far is? ->
            openList.Sort();
            PathFindingNode smallestCostSoFar = openList[0];

            //how do we get connections?
            //Debug.Log("weighted connections: " + smallestCostSoFar.graphNode.weightedConnections.Count);
            foreach (Node connectedNode in smallestCostSoFar.graphNode.weightedConnections.Keys)
            {

                if (!DoesListContainNode(connectedNode, closedList))
                {
                    if (!DoesListContainNode(connectedNode, openList))
                    {
                        float costToConnectedNode = smallestCostSoFar.costSoFar + smallestCostSoFar.graphNode.weightedConnections[connectedNode];
                        PathFindingNode predecessor = smallestCostSoFar;
                        //Debug.Log("adding node " + connectedNode);
                        pathfindingNodes.Add(connectedNode, new PathFindingNode(connectedNode, costToConnectedNode, predecessor));
                        openList.Add(pathfindingNodes[connectedNode]);
                    }
                    else
                    {
                        //is connection from the currently processing nod faster
                        //than the exitsting connection the this target node?
                        //if yes, update target node

                        //dijkstra
                        //float currentCostToTarget = pathfindingNodes[connectedNode].costSoFar;

                        //a*
                        float currentCostToTarget = pathfindingNodes[connectedNode].costSoFar + Vector3.Distance(connectedNode.position, toNode.position);

                        float costToTargetThroughCurrentNode = smallestCostSoFar.costSoFar + smallestCostSoFar.costSoFar + smallestCostSoFar.graphNode.weightedConnections[connectedNode];

                        if (costToTargetThroughCurrentNode < currentCostToTarget)
                        {
                            pathfindingNodes[connectedNode].costSoFar = costToTargetThroughCurrentNode;
                            pathfindingNodes[connectedNode].predecessor = smallestCostSoFar;
                        }
                    }
                }

                closedList.Add(smallestCostSoFar);
                openList.Remove(smallestCostSoFar);
            }//end while loop -pathfinding done
        }
        //destination is on closed list
        //[predessocrs olao on closed list
        //Debug.Log("toNode: " + toNode.ToString());
        //if (toNode == null) { Debug.Log("toNode is null."); }
        //Debug.Log("pathfinding nodes count: " + pathfindingNodes.Count);

        for (PathFindingNode waypoint = pathfindingNodes[toNode]; waypoint != null; waypoint = waypoint.predecessor)
        {
            waypoints.Add(waypoint.graphNode.position);
        }
        waypoints.Reverse();

        return waypoints;
    }//end pathfind

    private static bool DoesListContainNode(Node searchedNode, List<PathFindingNode> pathfindingNodeList)
    {
        foreach (PathFindingNode pathfindingNode in pathfindingNodeList)
        {
            if (pathfindingNode.graphNode == searchedNode)
            {
                return true;
            }
        }
        return false;
    }//end doeslistcontain
}//end class

//path findeing node class
public class PathFindingNode : System.IComparable<PathFindingNode>
{
    public Node graphNode;
    public float costSoFar;

    //predescor
    public PathFindingNode predecessor;

    public int CompareTo(PathFindingNode other)
    {
        return costSoFar.CompareTo(other.costSoFar);
    }//end compareTo

    public PathFindingNode(Node graphNode, float costSoFar, PathFindingNode predecessor)
    {
        this.graphNode = graphNode;
        this.costSoFar = costSoFar;
        this.predecessor = predecessor;
    }//end constructor

    public PathFindingNode(Node graphNode)
    {
        this.graphNode = graphNode;
        costSoFar = 0.0f;
        predecessor = null;
    }//end consturctoir
}//end class

public class Node
{
    public Node(Vector3 pos)
    {
        this.position = pos;
    }
    public Vector3 position;
    public Dictionary<Node, float> weightedConnections = new Dictionary<Node, float>();
    public override string ToString()
    {
        return this.position.ToString("F1");
    }
}//end class