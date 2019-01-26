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

    private void Start()
    {
        LookAtPos = FindCenterPoint(trackedPlayers.list.ToArray());
    }

    // Update is called once per frame
    void Update()
    {

        if (trackedPlayers.list.Count > 0)
        {
            cameraTragetPos = FindCenterPoint(trackedPlayers.list.ToArray());
            LookAtPos = Vector3.Lerp(LookAtPos, cameraTragetPos, Time.deltaTime * moveSpeedFactor);
            transform.LookAt(LookAtPos);
            if (debug)
            {
                Debug.DrawLine(transform.position, LookAtPos, Color.yellow);
                Debug.DrawLine(transform.position, cameraTragetPos, Color.green);
                for (int i = 0; i < trackedPlayers.list.Count; i++)
                {
                    Debug.DrawLine(trackedPlayers.list[i].transform.position, cameraTragetPos, Color.cyan);
                }
            }

            maxDistance = GetMaxDistance(trackedPlayers.list.ToArray(), cameraTragetPos, ref ob1);
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


    float GetMaxDistance(GameObject[] objects, Vector3 centerPoint, ref GameObject fartherObj)
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
                    fartherObj = objects[i];
                }
            }
        }
        return maxDistance;
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
