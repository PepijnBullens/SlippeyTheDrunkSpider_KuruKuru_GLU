using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GekkoBehaviorScript : MonoBehaviour
{
    [Header ("Moving Variables")]

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float distanceTolerance;


    //list of points gekko has to move to
    private List<Vector3> pathPoints = new List<Vector3>();
    //number of the point gekko is currently moving towards
    private int currentTargetedPathPoint = 0;

    //start
    private void Start()
    {
        //add the pathpoint gameobjects to the pathpoints list
        foreach(Transform child in transform.GetChild(0))
        {
            pathPoints.Add(child.transform.position);
        }

        //if there are less then 1 pathpoint (0 pathpoints). dont continue
        if(pathPoints.Count < 1) return; 

        //set gekko's position to first path point in list
        transform.position = pathPoints[currentTargetedPathPoint];
        //make gekko move to the next pathpoint
        currentTargetedPathPoint++;
    }

    //update
    private void Update()
    {
        //if there are less then 1 pathpoint (0 pathpoints). dont continue
        if (pathPoints.Count < 1) return;

        //
        Vector3 lookDirection = pathPoints[currentTargetedPathPoint] - transform.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, lookDirection.normalized);
        transform.Rotate(Vector3.forward, 180f);


        transform.position = Vector2.Lerp(transform.position, pathPoints[currentTargetedPathPoint], moveSpeed * Time.deltaTime);

        if(Vector2.Distance(transform.position, pathPoints[currentTargetedPathPoint]) < distanceTolerance)
        {
            if(currentTargetedPathPoint < pathPoints.Count - 1) currentTargetedPathPoint++;
            else currentTargetedPathPoint = 0;
        }
    }
}
