using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public float ghostSpeed;
    public List<Transform> roomWayPoints = new List<Transform>();

    private Transform currentTarget;
    private bool isMoving;

    void Start()
    {
        chooseTarget();
    }

    private void Update()
    {
        if(isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, ghostSpeed * Time.deltaTime);

            if (transform.position == currentTarget.position) //arrivedWayPoint
            {
                isMoving = false;
                chooseTarget();
            }
        }
    }

    private void chooseTarget()
    {
        currentTarget = roomWayPoints[Random.Range(0, roomWayPoints.Count)];
        isMoving = true;
    }
}
