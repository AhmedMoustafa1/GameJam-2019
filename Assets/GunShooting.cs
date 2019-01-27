using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooting : MonoBehaviour
{
    public GameObject BulletSpawner;
    public float damageValue=20;
    private float range = 100;
    public ParticleSystem muzzleFlash;
    public GameObject muzzleFlare;
    public CharacterControllerPoly charcter;
   
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButtonDown("JoystickF" + charcter.characterNum))
        {

            Shot();
        }
        Debug.DrawRay(BulletSpawner.transform.position, range* BulletSpawner.transform.forward, Color.red);

    }

    private void Shot()
    {
        muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(BulletSpawner.transform.position,BulletSpawner.transform.forward,out hit,range))
        {

            HoldEnemyHealth health = hit.transform.GetComponent<HoldEnemyHealth>();
            Debug.Log(hit.transform.gameObject.name);
            if (health !=null)
            {
                Debug.Log("wee");
                health.TakeDamage(damageValue);
            }
        }

       GameObject flare= Instantiate(muzzleFlare,hit.point,Quaternion.LookRotation(hit.normal));
        Destroy(flare,3);
    }
}
