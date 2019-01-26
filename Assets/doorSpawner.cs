using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorSpawner : MonoBehaviour
{
    public GameObject destination;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.position = destination.transform.position;
            other.transform.rotation = destination.transform.rotation;
        }
    }
}
