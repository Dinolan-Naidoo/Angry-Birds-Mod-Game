using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    //Public damage variable for the enemy that can be changed within the Unity inspector.
    public float maxDamage = 3f; 

    //Enemy death effect can be added in Unity inspector.
    public GameObject deathEffect;

    //Enemies alive integer (Used in die function).
    public static int EnemiesAlive = 0;

    private void Start()
    {
        //Finds all game objects with "Enemy" tag in scene.
        EnemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }
    public void OnCollisionEnter2D(Collision2D hitsomething)
    {
        //Checks the velocity and magnitude of a collision and relates the inequality with the Enemy maxDamage variable.
        if(hitsomething.relativeVelocity.magnitude > maxDamage)
        {
            //If the relative velocity and magnitude of the collision is greater than maxDamage , the enemy dies. (Die function called).
            Die();
        }
        
    }

    private void Update()
    {
      
    }

    

    public void Die()
    {
        //Creates a death effect at the instant of an enemy death.
        Instantiate(deathEffect, transform.position, Quaternion.identity);

        //Destroys the enemy (Note: All enemies have an "Enemy" tag).
        Destroy(gameObject);

        //Subtracts 1 from the EnemiesAlive integer variable.
        EnemiesAlive--;

        //Checks whether all enemies have been destroyed and calls the "Level Won" Scene.
        if (EnemiesAlive <= 0)
        {
            SceneManager.LoadScene("LevelWon");
        }

    }
}
