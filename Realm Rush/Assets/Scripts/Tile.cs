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
    PathFinder pathFinder;
    Vector2Int coordinates = new Vector2Int();

    void Awake() 
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
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
        if(gridManager == null)
        {
            Debug.Log("null");
        }else{
            Debug.Log("Not null");
        }

        if(pathFinder == null)
        {
            Debug.Log("null");
        }else{
            Debug.Log("Not null");
        }


        if(gridManager.GetNode(coordinates).isWalkable && !pathFinder.WillBlockPath(coordinates))
        {
            bool isPlaced =  towerPrefab.CreateTower(towerPrefab, transform.position); //This acts similar to Instantiate
            isPlaceable = !isPlaced;
            gridManager.BlockNode(coordinates);
        }
    }
}
