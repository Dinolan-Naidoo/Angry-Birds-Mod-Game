using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Hero2 : MonoBehaviour
{
    /// <Variables for player movement>
    private bool isPressed; // Checks whether an alien is selected.
    private Rigidbody2D playerRB; // Reference to player rigidbody.
    private SpringJoint2D springJ; // Reference to spring joint used as a slingshot. 
    private float releaseDelay; // A delay in the release of the alien from the slingshot.
    private float maxDragDistance = 2f; // Maximum distance an alien can be dragged (works as a radius around the spring joint).
    private Rigidbody2D slingRB; // Reference to slingshot rigidbody.
    private LineRenderer lineR; // Reference to line renderer.
    private TrailRenderer trailR;// Reference to trail renderer (follows alien).
    public GameObject nextPlayer; // Public game object in the inspector to set the next alien.
    public GameObject currentPlayer; // Public game object in the inspector to track current player.

    
    /// <Variables for player explosion>
    bool hasExploded = false; // True or false statement checking whether the alien has exploded. 
    public GameObject explosionEffect; // Public game object in the inspector in which an explosion effect can be added.
    public float blastRadius = 10f; // Radius of the explosion.
    public float force = 500f; // Force emitted by explosion.
    public float upliftModifer = 5; // Uplift force emitted by explosion.

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>(); //Gets the alien rigidbody.
        springJ = GetComponent<SpringJoint2D>();//Gets the spring rigidbody.
        releaseDelay = 1 / (springJ.frequency * 4); // Releases the alien from the spring joint at 1/4 of the SJ radius.
        slingRB = springJ.connectedBody; //Relates the sling rigidbody variable to the spring joint.
        lineR = GetComponent<LineRenderer>(); //Renders a line.
        trailR = GetComponent<TrailRenderer>(); // Follows the alien throughout its lifetime.
        lineR.enabled = false; // Initially set to false.
        trailR.enabled = false;// Initially set to false.
    }

    // Update is called once per frame
    void Update()
    {
        //Checks whether player has been selected.
        if (isPressed)
        {
            DragPlayer();
        }
    }

    //Sets isPressed to true , enables the line renderer and makes the alien rigidbody kinematic for smooth movement when aiming.
    private void OnMouseDown()
    {
        isPressed = true;
        playerRB.isKinematic = true;
        lineR.enabled = true;
    }

    //Sets isPressed to false , disables the line renderer , disables the kinetic rigidbody and starts the "release" coroutine.
    private void OnMouseUp()
    {
        isPressed = false;
        playerRB.isKinematic = false;
        StartCoroutine(release());
        lineR.enabled = false;
    }


    private void DragPlayer()
    {
        setLineposition();
        //Checks the mouse input
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Sets distance equal to the mouse position 
        float distance = Vector2.Distance(mousePosition, slingRB.position);

        //Limits the drag distance to the maxDragDistance value 
        if (distance > maxDragDistance)
        {
            Vector2 direction = (mousePosition - slingRB.position).normalized;
            playerRB.position = slingRB.position + direction * maxDragDistance;
        }
        else
        {
            playerRB.position = mousePosition;
        }
    }

    //Sets the line position between the alien and the slingshot.
    private void setLineposition()
    {
        Vector3[] positions = new Vector3[2];
        positions[0] = playerRB.position;
        positions[1] = slingRB.position;
        lineR.SetPositions(positions);
    }



    private IEnumerator release()
    {
        //Returns after the release delay is complete.
        yield return new WaitForSeconds(releaseDelay);

        //Spring joint disabled.
        springJ.enabled = false;

        //Trail renderer enabled (to follow alien).
        trailR.enabled = true;

        //Current player is active.
        currentPlayer.SetActive(true);

        //Waits 5 seconds before a new alien is added to the scene.
        yield return new WaitForSeconds(5f);

        //If the next player is true
        if (nextPlayer != null)
        {
            //Enable next alien in array.
            nextPlayer.SetActive(true);

            //Disable current alien in array.
            currentPlayer.SetActive(false);
        }

        else
        {
            //If there is no next player and enemies are still alive , the "Game Over" screen will load after 5 seconds.
            Invoke("gameOver", 4f);
        }

        this.enabled = false;

    }

    //Loads "Game Over" scene.
    public void gameOver()
    {
        SceneManager.LoadScene("Game Over");
    }

    //Creates an explosion with an effect as well as forces applied to nearby objects within the Blast radius
    void Explode()
    {
        // Creates the explosion effect.
        Instantiate(explosionEffect, transform.position, transform.rotation); 

        //Checks for collisions.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, blastRadius);

        foreach (Collider2D coll in colliders)
        {
            if (coll.GetComponent<Rigidbody2D>() && coll.name != "hero")
            {
                AddExplosionForce(coll.GetComponent<Rigidbody2D>(), force, transform.position, blastRadius, upliftModifer);
            }
        }

        
    }

    //Checks whether the hero has collided with an object in order to call the explode function
    public void OnCollisionEnter2D(Collision2D hasCollided)
    {
        if (!hasExploded) // Alien has not exploded yet.
        {
            Explode();
        }

        
        //Disables sprite renderer.
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        //Disables trail renderer.
        gameObject.GetComponent<TrailRenderer>().enabled = false;

        hasExploded = true;  // Alien has exploded.
    }


    // Adds explosion force to given rigidbody
    private void AddExplosionForce(Rigidbody2D body, float explosionForce, Vector3 explosionPosition, float explosionRadius, float upliftModifier = 0)
    {
        var dir = (body.transform.position - explosionPosition); // 'body = rigidbody to add force to ; 'explosionPosition' = location of the explosion source
        float wearoff = 1 - (dir.magnitude / explosionRadius); // 'explosionRadius' = radius of explosion effect
        Vector3 baseForce = dir.normalized * explosionForce * wearoff; // 'explosionForce' = base force of explosion
        baseForce.z = 0;
        body.AddForce(baseForce);

        if (upliftModifer != 0) // 'upliftModifier' = Additional upward force
        {
            float upliftWearoff = 1 - upliftModifier / explosionRadius;
            Vector3 upliftForce = Vector2.up * explosionForce * upliftWearoff;
            upliftForce.z = 0; // Limits the force to 2 dimensions (x,y).
            body.AddForce(upliftForce); // Adds the uplift force to the alien rigidbody. 
        }

    }
}
