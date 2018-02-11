using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {
	#region Fields
	public int numRows, numColums; //no of rows and columns in grid
	public float gridCellSize; //Note: we are using square need length
	public bool showGrid = true;
    public bool showObstacleBlocks;

    //for the nodes and grids
    public Node[,] nodes { get; set; }//2D array to store all nodes

	public Vector3 Origin
	{
		get{ return transform.position; }
	}

    #endregion

    //the following ensures singleton occurence of the GridManager
    //aka only one gridmanager is allowed kms

    //s_Instance is used to cache the instance found in the scene
    private static GridManager s_Instance = null;

    public static GridManager instance
    {
        //get and set
        get
        {
            if(s_Instance == null)
            {
                //this is where tha magic happens
                s_Instance = FindObjectOfType(typeof(GridManager)) as GridManager;
                //s_Instance = FindObjectOfType<GridManager>() as GridManager;  --> same as above
                if(s_Instance == null)
                {
                    Debug.LogError("cannot locate GridManager! No AStar");
                }
            }
            return s_Instance;
        }
    }

    //Upon awake, recalculate all obstacles (when the game is back in focus)
    private void Awake()
    {
        CalculateObstacle();
    }

    //get the neighbour in all 4 direction
    public void GetNeighbour(Node node, ArrayList neighbours)
    {
        //Get the current position of the node
        Vector3 neighbourPos = node.position;
        //get the index
        int neighbourIndex = GetGridIndex(neighbourPos);
        //get the rows and columns
        int row = GetRow(neighbourIndex);
        int column = GetColumn(neighbourIndex);
        //Assign the bottom
        AssignNeighbour(row + 1, column, neighbours);//bottom
        AssignNeighbour(row - 1, column, neighbours);//top
        AssignNeighbour(row, column - 1, neighbours);//left
        AssignNeighbour(row, column + 1, neighbours);//right
        //assign more neighbours for different direction
        AssignNeighbour(row + 1, column + 1, neighbours);
        AssignNeighbour(row - 1, column - 1, neighbours);
        AssignNeighbour(row + 1, column - 1, neighbours);
        AssignNeighbour(row - 1, column + 1, neighbours);
    }

    //checks the neighbouring cell, assign it as neighbour if it is not an obstacle(non-passable)
    void AssignNeighbour(int row, int column, ArrayList neighbours)
    {
        if(row != -1 && column != -1 && row < numRows && column < numColums)
        {
            Node nodeToAdd = nodes[row, column];
            if (!nodeToAdd.bObstacle)
            {
                neighbours.Add(nodeToAdd);//add if not obstacle
            }
        }
    }

    //Calculate All Obstacles in the map
    void CalculateObstacle()
    {
        //initialise the node array
        nodes = new Node[numRows, numColums];
        //helper variables
        int index = 0;//to keep track of the nodes visited
        int layer = 1 << 16;//this is the layer for the collision matrix
        Vector3 cellSize = new Vector3(gridCellSize, 1.0f, gridCellSize);//Sphere position
        for (int i = 0; i < numColums; i++)
        {
            for (int j = 0; j < numRows; j++)
            {
                //Given index get position
                Vector3 cellPos = GetGridCellCenter(index);//Sphere position
                Node node = new Node(cellPos);
                //check for obstacle
                if(Physics.CheckSphere(cellPos, cellSize.x/2, layer))
                {
                    node.MarkAsObstacle();//Found obstacle
                }
                index++;
                nodes[i, j] = node;//store the node in the array;
            }
        }

    }

    //Given an index, find the center of the cell (Vector3)
    public Vector3 GetGridCellCenter(int index)
    {
        Vector3 cellPosition = GetGridCellPosition(index);
        cellPosition.x += gridCellSize / 2.0f;
        cellPosition.z += gridCellSize / 2.0f;
        return cellPosition;
    }

        //Given an index, find the position(Vector3)
        public Vector3 GetGridCellPosition(int index)
    {
        int row = GetRow(index);
        int column = GetColumn(index);
        //Get the reference coordinate
        float xPosInGrid = column * gridCellSize;
        float zPosInGrid = row * gridCellSize;
        //Remember to add the Origin offset
        return Origin + new Vector3(xPosInGrid, 0, zPosInGrid);
    }

    //Given an index, find the row and columns
    public int GetRow(int index)
    {
        return index / numColums;//Note that this is INTEGER division
    }

    public int GetColumn(int index)
    {
        return index % numColums;
    }

    //Given a position, we need to  find the Grid index
    public int GetGridIndex(Vector3 pos)
    {
        //Check if the pos is within the grid

        //If it is then find the row and column
        pos -= Origin;
        int col = (int)(pos.x / gridCellSize);// get the col based on coord Note:square grid
        int row = (int)(pos.z / gridCellSize);// get the col based on coord Note:square grid
        return row * numColums + col;//return the grid index formula: row * nC + col;
    }
    //Check if a pos (Coord) is within the grid
    public bool IsInBound(Vector3 pos)
    {
        float length = numColums * gridCellSize;
        float breadth = numRows * gridCellSize;
        //pos -= Origin;//align it to the origin of the grid *IMPORTANT*
        //Use AABB collition intersection algorithm
        return (
            pos.x >= Origin.x &&
            pos.x <= Origin.x + length &&
            pos.z >= Origin.z &&
            pos.z <= Origin.z + breadth
            );
        
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnDrawGizmos()
	{
        Vector3 cellSize = new Vector3(gridCellSize, 1, gridCellSize);
		if (showGrid) {
			DebugDrawGrid ();
		}
        if (showObstacleBlocks)
        {
            for (int i = 0; i < numColums; i++)
            {
                for (int j = 0; j < numRows; j++)
                {
                    if (nodes != null && nodes[i,j].bObstacle)
                    {
                        //Get index
                        int index = GetGridIndex(nodes[i, j].position);
                        //Given index get position
                        Vector3 cellPos = GetGridCellCenter(index);//Sphere position
                        //Draw the obstacle node
                        Gizmos.DrawCube(cellPos, cellSize);
                        //Gizmos.DrawSphere(cellPos, gridCellSize / 2);
                    }
                }
            }
        }
	}
	private void DebugDrawGrid()
	{
		Vector3 origin = transform.position;
		float width = numColums * gridCellSize;
		float length = numRows * gridCellSize;
		Color color = Color.yellow;
		//draw all horizontal lines
		for (int i = 0; i < numRows; i++) {
			Vector3 startPos = origin + i * gridCellSize * new Vector3 (0, 0, 1);
			Vector3 endPos = startPos + width * new Vector3 (1, 0, 0);
			Debug.DrawLine (startPos, endPos, color);
		
		}
        //draw vertical line
        for (int i = 0; i < numColums; i++)
        {
            Vector3 startPos = origin + i * gridCellSize * new Vector3(1, 0, 0);
            Vector3 endPos = startPos + width * new Vector3(0, 0, 1);
            Debug.DrawLine(startPos, endPos, color);

        }
    }
}
