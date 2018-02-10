using System;
using System.Collections;
using UnityEngine;

//namespace Assets.Script
//{
public class PriorityQueue
{
    //First: We need a place to store the nodes
    private ArrayList nodes = new ArrayList();
    //Need to know the number of nodes
    public int Length
    {
        get { return nodes.Count; }
    }
    //Need a compare method to check if a node is in the array
    public bool Contains(object node)
    {
        return nodes.Contains(node);//This is a method in ArrayList
    }

    //Get the first node in the queue
    public Node First()
    {
        if(nodes.Count > 0)
        {
            return (Node)nodes[0];
        }
        return null;
    }

    //add new node into the queue
    public void Push(Node node)
    {
        nodes.Add(node);//normal queue ends here
        nodes.Sort();//This is for Priority Queues : need IComparable interface
    }

    public void Remove(Node node)
    {
        nodes.Remove(node);//normal queue ends here
        //nodes.Sort();//This is for Priority Queues : need IComparable interface
    }
}
//}
