using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    //Start and end coordinates
    [SerializeField] Vector2Int startCoordinates;
    [SerializeField] Vector2Int destinationCoordinates;

    //Properties
    public Vector2Int StartCoordinates{ get {return startCoordinates;}}
    public Vector2Int DestinationCoordinates{ get {return destinationCoordinates;}}

    Node startNode;
    Node destinationNode;
    Node currentSearchNode;
    Vector2Int[] directions = {Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down};

    //Reference GridManager
    GridManager gridManager = new GridManager();

    Dictionary<Vector2Int, Node> grid;
    //If the Node has being explored
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
    Queue<Node> frontier = new Queue<Node>();

    void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null)
        {
            //Set grid to class gridManager
            grid = gridManager.Grid;
                    //Grab Starting/ending point from dictionary
        //----------class . dictinary [entry]
        startNode = grid[startCoordinates];
        destinationNode = grid[destinationCoordinates];
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        GetNewPath();
    }

    public List<Node> GetNewPath()
    {
        gridManager.ResetNodes();
        BreathFirstSearch();

        return BuildPath();
    }

    void ExploredNeighbors()
    {
        List<Node> neighbors = new List<Node>();

        foreach(Vector2Int direction in directions){
            Vector2Int neighborCoords = currentSearchNode.coordinates + direction;

            if(grid.ContainsKey(neighborCoords))
            {
                neighbors.Add(grid[neighborCoords]);
            }
        }

        foreach(Node neighbor in neighbors)
        {
            if (!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                //Create connections for the map
                neighbor.connectedTo = currentSearchNode;

                reached.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    void BreathFirstSearch()
    {
        startNode.isWalkable = true;
        destinationNode.isWalkable = true;
        //Restart for pathfinding (2nd time)
        frontier.Clear();
        reached.Clear();

        bool isRunning = true;

        frontier.Enqueue(startNode);
        reached.Add(startCoordinates, startNode);

        while(frontier.Count > 0 && isRunning == true){
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploredNeighbors();
            if(currentSearchNode.coordinates == destinationCoordinates)
            {
                isRunning = false;
            }
        }

    }

    //Return a list of Nodes
    List<Node> BuildPath()
    {
       List<Node> path =  new List<Node>();
       Node currentNode = destinationNode;

       path.Add(currentNode);
       currentNode.isPath = true;

        //While there still connected Nodes to explore
       while(currentNode.connectedTo != null)
       {
        //Takes us one step back in the node
        currentNode = currentNode.connectedTo;

        path.Add(currentNode);
        currentNode.isPath = true;
       }

    path.Reverse();

    return path;
    }

    public bool WillBlockPath(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            bool previousState = grid[coordinates].isWalkable;

            grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();
            grid[coordinates].isWalkable = true;

            if (newPath.Count <=1)
            {
                GetNewPath();
                return true;
            }
            return false;
        }
        return false;
    }

}
