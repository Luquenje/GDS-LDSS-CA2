using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

    private Vector3 startPos, endPos;//Actual recalculated start and end pt
    public Transform start, end;//Denote the start and end object in the scene
    public ArrayList pathArray;//Calculate path
    public AStarSteeringBehaviour enemy;
    public GameObject marker;
	// Use this for initialization
	void Start () {
		if(start != null && end != null)
        {
            Initialise();
        }
	}
    //public void SetEndPos(Vector3 EndPos)
    //{
    //    endPos = EndPos;
    //}
	

    void Initialise()
    {
        FindPath();//Get the path
    }
    //get AStar to find the path
    public void FindPath()
    {
        startPos = start.position;//
        endPos = end.position;
        //Assign start and end node
        Vector3 s = GridManager.instance.GetGridCellCenter(GridManager.instance.GetGridIndex(startPos));
        Node startNode = new Node(s);

        Vector3 e = GridManager.instance.GetGridCellCenter(GridManager.instance.GetGridIndex(endPos));
        Node endNode = new Node(e);

        pathArray = AStar.FindPath(startNode, endNode);
        enemy.waypoints = pathArray;

        //foreach(Node node in pathArray)
        //{
        //    GameObject obj = Instantiate(marker, node.position, Quaternion.identity);
        //    tank.waypoints[i] = obj.transform;
        //    i++;
        //}
        enemy.currentWaypoint = 0;
        enemy.currentState = AStarSteeringBehaviour.AIState.WAYPOINTS;
        enemy.moveSpeed = 6.0f;
        //Debug.Log(pathArray.Count);//debug just to make sure AStar does something


    }

    void OnDrawGizmos()
    {
        if (pathArray == null) return;// do nothing if no path
        if(pathArray.Count > 0) // draw the path
        {
            int index = 1;
            foreach(Node node in pathArray)
            {
                if (index < pathArray.Count)
                {
                    Node nextNode = (Node)pathArray[index];
                    Debug.DrawLine(node.position, nextNode.position, Color.red);
                    index++;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Player mouse click
        //RaycastHit hit;
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit, 200.0f))
        //{
        //    Debug.Log(hit.point);
        //    end.position = hit.point;
        //    FindPath();
        //}
        //FindPath();
    }
}
