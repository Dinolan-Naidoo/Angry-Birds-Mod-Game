using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverScreen : MonoBehaviour
{
    //Loads the start scene when the "Main Menu" button is pressed.
    public void returnToMainMenu()
    {
        SceneManager.LoadScene("StartScene");
    }

    //Loads the levels scene when the "Choose Level" button is pressed.
    public void Restart()
    {
        SceneManager.LoadScene("Levels");
    }
}
