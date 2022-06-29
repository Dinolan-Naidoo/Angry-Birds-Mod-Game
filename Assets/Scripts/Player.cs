using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    /// <Variables for player movement>
    private bool isPressed;// Checks whether an alien is selected.
    private Rigidbody2D playerRB;// Reference to player rigidbody.
    private SpringJoint2D springJ;// Reference to spring joint used as a slingshot.
    private float releaseDelay;// A delay in the release of the alien from the slingshot.
    private float maxDragDistance = 2f;// Maximum distance an alien can be dragged (works as a radius around the spring joint).
    private Rigidbody2D slingRB;// Reference to slingshot rigidbody.
    private LineRenderer lineR;// Reference to line renderer.
    private TrailRenderer trailR;// Reference to trail renderer (follows alien).
    public GameObject nextPlayer;// Public game object in the inspector to set the next alien.
    public GameObject currentPlayer;// Public game object in the inspector to track current player.

    
    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();//Gets the alien rigidbody.
        springJ = GetComponent<SpringJoint2D>();//Gets the spring rigidbody.
        releaseDelay = 1 / (springJ.frequency * 4);// Releases the alien from the spring joint at 1/4 of the SJ radius.
        slingRB = springJ.connectedBody;//Relates the sling rigidbody variable to the spring joint.
        lineR = GetComponent<LineRenderer>();//Renders a line.
        trailR = GetComponent<TrailRenderer>();//Renders a trail.(follows alien)
        lineR.enabled = false;// Initially set to false.
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
        //Sets position of line when alien is dragged. 
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
        Vector3[] positions = new Vector3[2];//Two positions where the line is rendered.
        positions[0] = playerRB.position;//Line from alien.
        positions[1] = slingRB.position;//Line to slingshot.
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

}
    
    
