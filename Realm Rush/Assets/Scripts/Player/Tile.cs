using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable;
    //Property isPlaceable
    public bool IsPlaceable
    {
        get{ return isPlaceable; }
    }

    GridManager gridManager;
    PathFinder pathfinder;
    Vector2Int coordinates = new Vector2Int();

    void Awake() 
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<PathFinder>();
    }

    void Start() 
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }

        }
    }

    void OnMouseDown()
    {
        if(gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates))
        {
            //Creation of the tower is successful
            bool isSuccessful =  towerPrefab.CreateTower(towerPrefab, transform.position); //This acts similar to Instantiate
            if (isSuccessful){
            gridManager.BlockNode(coordinates);
            pathfinder.NotifyReceivers();
            }
        }
    }
}
