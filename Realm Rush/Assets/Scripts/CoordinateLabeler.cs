using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color bloquedColor = Color.gray;

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int(); //Representation of a 2d vector using int
    Waypoint waypoint;

    void Awake() 
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        DisplayCoordinates();    
        waypoint = GetComponentInParent<Waypoint>();
    }
    
    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }

        SetLabelColor();
        ToggleLabels();
    }

    void SetLabelColor()
    {
        if (waypoint.IsPlaceable)
        {
            label.color = defaultColor;
        }else
        {
            label.color = bloquedColor;
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
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / 10); //UnityEditor.EditorSnapSettings.move.x
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / 10); //UnityEditor.EditorSnapSettings.move.z
        label.text = coordinates.x + ", " + coordinates.y;
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
