using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.red;
    [SerializeField] Color exploredColor = Color.yellow;
    //Create a orange color
    [SerializeField] Color pathColor = new Color(1f, 0.5f, 0f);


    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int(); //Representation of a 2d vector using int
    GridManager gridManager;

    void Awake() 
    {
        //Order of execution matters
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        DisplayCoordinates();    
    }
    
    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
            //For testing editor mode
            label.enabled = true;
        }

        SetLabelColor();
        ToggleLabels();
    }

    void SetLabelColor()
    {
        if(gridManager == null) { return; }

        Node node = gridManager.GetNode(coordinates);

        if(node == null) { return; }

        if(!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if(node.isPath)
        {
            label.color = pathColor;
        }
        else if(node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }
    }

    void ToggleLabels()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            label.enabled = !label.IsActive();
        }
    }

    void DisplayCoordinates()
    {
        if (gridManager == null){ return ; }
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize); //UnityEditor.EditorSnapSettings.move.x
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize); //UnityEditor.EditorSnapSettings.move.z
        label.text = coordinates.x + ", " + coordinates.y;
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
