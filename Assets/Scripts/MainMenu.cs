using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    //Loads the levels scene when the "Play" button is pressed.
    public void playGame()
    {
        SceneManager.LoadScene("Levels");
    }

    //Exits the game.
    public void quitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
