﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooting : MonoBehaviour
{
    public GameObject BulletSpawner;
    public float damageValue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

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
        RaycastHit hit;
        if (Physics.Raycast(BulletSpawner.transform.position,BulletSpawner.transform.forward,out hit))
        {

            HoldEnemyHealth health = hit.transform.GetComponent<HoldEnemyHealth>();
          
            if (health !=null)
            {
           
                health.TakeDamage(damageValue);
            }
        }
     
    }
}