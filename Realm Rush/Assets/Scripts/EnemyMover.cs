using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    List<Node> path = new List<Node>();
    [SerializeField] [Range(0f, 5f)] float speed = 1f;

    Enemy enemy;
    PathFinder pathfinder;
    GridManager gridmanager;

    void Awake() 
    {
        enemy = GetComponent<Enemy>();
        gridmanager = FindObjectOfType<GridManager>();
        pathfinder =FindObjectOfType<PathFinder>();
    }

    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    void FindPath()
    {
        path.Clear();

        path = pathfinder.GetNewPath();
    }

    void ReturnToStart()
    {
        transform.position = gridmanager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }

    IEnumerator FollowPath() 
    {
        for(int i = 0; i<path.Count; i++) 
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridmanager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while(travelPercent < 1f) {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

        FinishPath();
        
    }

}

