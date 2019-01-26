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

    GameObject ob1;
    GameObject ob2;
    float maxDistance;
    Vector3 cameraTragetPos;
    float cameraTragetFOV;
    Camera cam;
    Vector3 LookAtPos;
    // Start is called before the first frame update
    void Awake()
    {
        cam = GetComponent<Camera>();
    }



    // Update is called once per frame
    void Update()
    {
        if ((maxDistance = MaxDistance(trackedPlayers.list.ToArray(), ref ob1, ref ob2)) > 0)
        {
            cameraTragetPos = Vector3.Lerp(ob1.transform.position, ob2.transform.position, .5f);
        }
        else if (trackedPlayers.list.Count == 1)
        {
            cameraTragetPos = trackedPlayers.list[0].transform.position;
        }
        else
        {
            return;
        }

        LookAtPos = Vector3.Lerp(LookAtPos, cameraTragetPos, Time.deltaTime * moveSpeedFactor);
        transform.LookAt(LookAtPos);
        if (debug)
        {
            Debug.DrawLine(transform.position, LookAtPos, Color.yellow);
            Debug.DrawLine(transform.position, cameraTragetPos, Color.green);
        }

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

        if (debug)
        {
            for (int i = 0; i < trackedPlayers.list.Count; i++)
            {
                Debug.DrawLine(trackedPlayers.list[i].transform.position, cameraTragetPos, Color.cyan);
            }
        }
    }


    float MaxDistance(GameObject[] objects, ref GameObject object1, ref GameObject object2)
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
                        object1 = objects[x];
                        object2 = objects[y];
                    }
                }
            }
        }

        return (maxDistance);

    }
}
