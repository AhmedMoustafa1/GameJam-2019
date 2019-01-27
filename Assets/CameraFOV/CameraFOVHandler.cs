using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFOVHandler : MonoBehaviour
{

    public GameObjectsListVariable trackedPlayers;
    public float moveSpeedFactor;
    public float zoomSpeedFactor;
    public float adjustFOVFactor;
    public float minFOV;
    public float MaxFOV;
    public bool debug;


    float maxDistance;

    float cameraTragetFOV;
    Camera cam;
    Vector3 originalCenter;
    Vector3 OriginalPosition;

    Vector3 newCenter;
    Vector3 newPosition;

    // Start is called before the first frame update
    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Start()
    {
        originalCenter = FindCenterPoint(trackedPlayers.list.ToArray());
        OriginalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (trackedPlayers.list.Count > 0)
        {
            newCenter = FindCenterPoint(trackedPlayers.list.ToArray());
            newPosition = OriginalPosition + (newCenter - originalCenter);
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * moveSpeedFactor);


            // transform.position += (newCenter - originalCenter);
            if (debug)
            {
                Debug.DrawLine(newCenter, originalCenter, Color.yellow);
                Debug.DrawLine(OriginalPosition, newPosition, Color.yellow);
                Debug.DrawLine(newCenter, newPosition, Color.green);
                Debug.DrawLine(originalCenter, OriginalPosition, Color.gray);

                for (int i = 0; i < trackedPlayers.list.Count; i++)
                {
                    Debug.DrawLine(trackedPlayers.list[i].transform.position, newCenter, Color.cyan);
                    //  Debug.DrawLine(trackedPlayers.list[i].transform.position, originalCenter, Color.gray);
                }
            }
            //   originalCenter = newCenter;

            maxDistance = GetMaxDistance(trackedPlayers.list.ToArray());
            cameraTragetFOV = (maxDistance * adjustFOVFactor);
            if (cameraTragetFOV < minFOV) cameraTragetFOV = minFOV;
            else if (cameraTragetFOV > MaxFOV) cameraTragetFOV = MaxFOV;
            if (cam.orthographic)
            {
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, cameraTragetFOV, Time.deltaTime * zoomSpeedFactor);
            }
            else
            {
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, cameraTragetFOV, Time.deltaTime * zoomSpeedFactor);
            }

        }

    }


    float GetMaxDistance(GameObject[] objects, Vector3 centerPoint)
    {
        float maxDistance = -1f;
        float tempDestance = 0;
        if (objects.Length > 1)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                tempDestance = Vector3.Distance(centerPoint, objects[i].transform.position);
                if (tempDestance > maxDistance)
                {
                    maxDistance = tempDestance;
                }
            }
        }
        return maxDistance;
    }


    GameObject debugObj1;
    GameObject debugObj2;
    float GetMaxDistance(GameObject[] objects)
    {
        float maxDistance = -1f;
        float tempDestance = 0;
        if (objects.Length > 1)
        {
            for (int x = 0; x < objects.Length - 1; x++)
            {
                for (int y = x + 1; y < objects.Length; y++)
                {
                    if (maxDistance < (tempDestance = Vector3.Distance(objects[x].transform.position, objects[y].transform.position)))
                    {
                        maxDistance = tempDestance;
                        if (debug)
                        {
                            debugObj1 = objects[x];
                            debugObj2 = objects[y];

                        }
                    }
                }
            }
        }
        if (debug)
        {
            Debug.DrawLine(debugObj1.transform.position, debugObj2.transform.position, Color.red);
            Debug.DrawLine(transform.position, Vector3.Lerp(debugObj1.transform.position, debugObj2.transform.position, .5f), Color.red);
        }
        return (maxDistance);

    }


    Vector3 FindCenterPoint(GameObject[] gos)
    {
        Vector3 center = new Vector3(0, 0, 0);
        for (int i = 0; i < gos.Length; i++)
        {
            center += gos[i].transform.position;
        }
        return center / gos.Length;
    }
}
