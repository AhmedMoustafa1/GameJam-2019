using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    public int roomNum;
    public WayPoints roomWayPoints;
    public GameObject ghostPrefab;
    public Vector2 timeRange;
    private EnemiesManager enemiesManager;

    private float currentWaitTime;

    void Start()
    {
        enemiesManager = FindObjectOfType<EnemiesManager>();
        ChooseWaitinTime();
    }

    private void ChooseWaitinTime()
    {
        currentWaitTime = Random.Range(timeRange.x, timeRange.y);
        StartCoroutine(SpawnGhost());
    }


    private IEnumerator SpawnGhost()
    {
        yield return new WaitForSeconds(currentWaitTime);
        GameObject ghostTemp = Instantiate(ghostPrefab, transform.position, Quaternion.EulerAngles(0,0,90));
        ghostTemp.GetComponent<GhostRoomNum>().ghostRoomNum = roomNum;
        ghostTemp.GetComponent<GhostMovement>().roomWayPoints = roomWayPoints.waypoints;
        ChooseWaitinTime();
        enemiesManager.ghostsMeshRenders.Add(ghostTemp.GetComponent<MeshRenderer>());
    }
}
