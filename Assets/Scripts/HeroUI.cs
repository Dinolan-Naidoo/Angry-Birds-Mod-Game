using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroUI : MonoBehaviour
{
    //Public array of alien heroes in the scene.
    public GameObject[] playersLeft;

    //Public integer indicating players life. 
    public int playersLife;

    //The following controls the user-interface implemented in each level that sets the amount of alien heroes the player has and decreases it as each alien is used.
    public void UIControl()
    {

        playersLife -= 1;
        for (int i = 0; i < playersLeft.Length; i++)
        {
            if (playersLife > i)
            {
                playersLeft[i].SetActive(true);
            }
            else
            {
                playersLeft[i].SetActive(false);
            }

        }
    }
}
