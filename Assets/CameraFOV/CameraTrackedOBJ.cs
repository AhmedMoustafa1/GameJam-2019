using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrackedOBJ : MonoBehaviour
{
    public GameObjectsListVariable CameraTrackedObjects;

    private void OnEnable()
    {
        CameraTrackedObjects.Add(this.gameObject);
    }
    private void OnDisable()
    {
        CameraTrackedObjects.Remove(this.gameObject);
    }
}
