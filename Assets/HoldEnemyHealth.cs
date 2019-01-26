using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldEnemyHealth : MonoBehaviour
{
    public float health=40;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <=0)
        {
            Destroy(this.gameObject);
        }
    }
}
