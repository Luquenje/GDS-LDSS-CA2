using System;
using System.Collections;

public class AStar
{
    #region List fields
    public static PriorityQueue openList, closedList;
    #endregion

    //calculate estimated cost(heuristic) to the goal
    //We use Euclidean distance here
    public static float HeuristicEstimateCost(Node curNode, Node goalNode)
    {
        //this is the one that decides which node gets prioritised
        return (curNode.position - goalNode.position).magnitude;
    }

    //Here is the meat of AStar called FindPath!!
    public static ArrayList FindPath(Node start, Node goal)
    {
        //Start Finding the path
        //Create the openList and closedList
        openList = new PriorityQueue();
        closedList = new PriorityQueue();
        //add the start node to openList
        openList.Push(start);
        start.nodeTotalCost = 0.0f;//zero cost to reach start point
        start.estimatedCost = HeuristicEstimateCost(start, goal);
        //Working node object
        Node node = null;
        while(openList.Length != 0)//there is still unvisited node
        {
            //Get the high priority node
            node = openList.First();
            //Check if we have reach goal
            if(node.position == goal.position)
            {
                return CalculatedPath(node);
            }
            //not yet reach, do more visiting
            ArrayList neighbours = new ArrayList();
            GridManager.instance.GetNeighbour(node, neighbours);//get visitable node
            #region Check Neighbour
            //Get each of the neighbour
            for (int i = 0; i < neighbours.Count; i++)
            {
                Node neighbourNode = (Node)neighbours[i];
                //Make sure neighbour not in closedList
                if (!closedList.Contains(neighbourNode))
                {
                    //Get all G, H F values for the node
                    float cost = HeuristicEstimateCost(node, neighbourNode);
                    //Total cost from start to this neighbour node
                    float totalCost = node.nodeTotalCost + cost;
                    //Estimated cost from neighbour to goal
                    float neighbourNodeEstCost = HeuristicEstimateCost(neighbourNode, goal);
                    //Assign the parameter
                    neighbourNode.nodeTotalCost = totalCost;
                    neighbourNode.parent = node;
                    neighbourNode.estimatedCost = totalCost + neighbourNodeEstCost;
                    //Add it to openList
                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Push(neighbourNode);
                    }
                }
            }
            #endregion
            closedList.Push(node);//add current node to visited list
            openList.Remove(node);//remove current node availability to visited
            //If finished looping and cannot find goal return null
            if(node.position == goal.position) { return null; }

            
        }
        return CalculatedPath(node);
    }

    //Track back the route after reaching the goal node
    private static ArrayList CalculatedPath(Node node)
    {
        ArrayList list = new ArrayList();
        while(node != null)
        {
            list.Add(node);
            node = node.parent;
        }
        list.Reverse();
        return list;
    }
}
