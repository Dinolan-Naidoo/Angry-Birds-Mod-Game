using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle2 : MonoBehaviour
{
    //Public maximum damage variable that can be edited in the inspector.
    public float maxDamage = 3f;

    //Public death effect variable that can be edited in the inspector.
    public GameObject deathEffect;



    private void Start()
    {


    }

    //Checks whether the velocity and magnitude of the collision is greater or less than the maximum damage variable.
    void OnCollisionEnter2D(Collision2D hitsomething)
    {
        //If the velocity and magnitude of the collision is greater than the maximum damage variable , the object is destroyed.
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
        //Creates a death effect at the instant of the object death.
        Instantiate(deathEffect, transform.position, Quaternion.identity);

        //Destroys the object.
        Destroy(gameObject);
    }
}
