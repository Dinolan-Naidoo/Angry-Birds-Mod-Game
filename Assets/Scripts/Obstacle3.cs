using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle3 : MonoBehaviour
{

    public float maxDamage = 3f;
    public GameObject deathEffect;



    private void Start()
    {


    }
    private void OnCollisionEnter2D(Collision2D hitsomething)
    {
        if (hitsomething.relativeVelocity.magnitude > maxDamage)
        {
            Die();
        }

    }

    private void Update()
    {

    }



    public void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


}
