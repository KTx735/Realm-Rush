using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
   public Vector2Int coordinates;

    //Flags
    //If node can be added to the tree
    public bool isWalkable;
    //If the node was already explored
    public bool isExplored;
    //If node is on the path
    public bool isPath;
    public Node connectedTo;

    //Constructor for Node
    public Node(Vector2Int coordinates, bool isWalkable)
    {
        this.coordinates = coordinates;
        this.isWalkable = isWalkable;
    }

}
