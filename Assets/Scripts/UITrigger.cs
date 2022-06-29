using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        //Checks the tag of the Hero object that collides with the trigger and calls the UI Control function in the HeroUI script.
        if (other.tag == "Hero1")
        {
            FindObjectOfType<HeroUI>().UIControl();
        }

        //Checks the tag of the Hero object that collides with the trigger and calls the UI Control function in the HeroUI script.
        if (other.tag == "Hero2")
        {
            FindObjectOfType<HeroUI>().UIControl();
        }
    }
}
