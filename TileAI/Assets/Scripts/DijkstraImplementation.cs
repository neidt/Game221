using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DijkstraImplementation
{
    public static List<Vector3> Pathfind(/*TileGraph graph,*/ Node fromNode, Node toNode)
    {
        List<Vector3> waypoints = new List<Vector3>();

        //todo: do pathfinding algorithm

        List<PathFindingNode> openList = new List<PathFindingNode>();
        List<PathFindingNode> closedList = new List<PathFindingNode>();

        Dictionary<Node, PathFindingNode> pathfindingNodes = new Dictionary<Node, PathFindingNode>();

        pathfindingNodes.Add(fromNode, new PathFindingNode(fromNode));
        openList.Add(pathfindingNodes[fromNode]);
        
        while (openList.Count > 0 && !DoesListContainNode(toNode, closedList))
        {
            //todo: find connections from the lowest cost-so-far point to all connected points

            //how do you know what the smallest cost-so-far is? ->
            openList.Sort();
            PathFindingNode smallestCostSoFar = openList[0];

            //how do we get connections?
            foreach (Node connectedNode in smallestCostSoFar.graphNode.connections.Keys)
            {
                if (!DoesListContainNode(connectedNode, closedList))
                {
                    if (!DoesListContainNode(connectedNode, openList))
                    {
                        float costToConnectedNode = smallestCostSoFar.costSoFar + smallestCostSoFar.graphNode.connections[connectedNode];
                        PathFindingNode predecessor = smallestCostSoFar;

                        pathfindingNodes.Add(connectedNode, new PathFindingNode(connectedNode, costToConnectedNode, predecessor));
                        openList.Add(pathfindingNodes[connectedNode]);
                    }
                    else
                    {
                        //is connection from the currently processing nod faster
                        //than the exitsting connection the this target node?
                        //if yes, update target node
                        float currentCostToTarget = pathfindingNodes[connectedNode].costSoFar;
                        float costToTargetThroughCurrentNode = smallestCostSoFar.costSoFar + smallestCostSoFar.costSoFar + smallestCostSoFar.graphNode.connections[connectedNode];

                        if(costToTargetThroughCurrentNode < currentCostToTarget)
                        {
                            pathfindingNodes[connectedNode].costSoFar = costToTargetThroughCurrentNode;
                            pathfindingNodes[connectedNode].predecessor = smallestCostSoFar;
                        }
                    }
                }
            }
            closedList.Add(smallestCostSoFar);
            openList.Remove(smallestCostSoFar);
        }//end while loop -pathfinding done

        //todoL: fill out waypoints
        //destination is on closed list
        //[predessocrs olao on closed list
        for(PathFindingNode waypoint = pathfindingNodes[toNode]; waypoint!= null; waypoint = waypoint.predecessor)
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

/*
public class TileGraph
{
    public List<Vector3> points = new List<Vector3>();
}//end class*/

public class Node
{
    public Vector3 position;
    public Dictionary<Node, float> connections = new Dictionary<Node, float>();
}//end class