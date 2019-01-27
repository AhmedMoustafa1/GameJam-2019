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
   
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shot();
        }
    }

    private void Shot()
    {
        muzzleFlash.Play();
        RaycastHit hit;
        Debug.DrawRay(BulletSpawner.transform.position, BulletSpawner.transform.forward,Color.red);
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

        Instantiate(muzzleFlare,hit.point,Quaternion.LookRotation(hit.normal));
    }
}
