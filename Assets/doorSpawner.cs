using System.Collections;
using UnityEngine;

public class doorSpawner : MonoBehaviour
{
    public GameObject destination;
    public CharacterControllerPoly chara;
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
            chara = other.gameObject.GetComponent<CharacterControllerPoly>();
            StartCoroutine(stop());
            other.transform.position = destination.transform.position;
            other.transform.forward = destination.transform.forward;
        }
    }

    IEnumerator stop()
    {
        chara.enabled=false;

        yield return new WaitForSeconds(.5f);
        chara.enabled = true;

    }
}
