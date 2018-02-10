using System;
using System.Collections;
using UnityEngine;

//namespace Assets.Script
//{
public class Node:IComparable
{
    #region Fields
    public float nodeCost;
    public float nodeTotalCost;//total cost so far to reach this node
    public float estimatedCost;//Est cost from this node to destination(Goal)
    public bool bObstacle;//to mark this as an obstacle(passable/non passable)
                         //this will be the node cost later
    public Node parent;// parent of this node in the linked list
    public Vector3 position;//this is the position of the node in the scene
    #endregion

    public Node()
    {
        this.nodeCost = 1.0f;
        this.estimatedCost = 0.0f;
        this.nodeTotalCost = 1.0f;
        this.bObstacle = false;
        this.parent = null;
        this.position = Vector3.zero;
    }

    public Node(Vector3 p)
    {
        this.estimatedCost = 0.0f;
        this.nodeTotalCost = 1.0f;
        this.bObstacle = false;
        this.parent = null;
        this.position = p;
    }
    //mark as override

    public void MarkAsObstacle()
    {
        this.bObstacle = true;
    }
    //This is the implementation of the iterface IComparable
    //The PriorityQueue will help to sort the node based on the estimated cost of the node

    public int CompareTo(object obj)
    {
        Node node = (Node)obj;
        if(this.estimatedCost < node.estimatedCost)
        {
            return -1;
        }
        else if (this.estimatedCost > node.estimatedCost)
        {
            return 1;
        }
        return 0;
    }
}
//}
