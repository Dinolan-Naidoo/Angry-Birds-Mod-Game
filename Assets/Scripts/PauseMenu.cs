using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //True or false statement setting the variable to false initially.
    public static bool gamePaused = false;

    //Public game object in inspector where a pause menu is added. 
    public GameObject pauseMenuUI;
   
    private void Update()
    {
        //Sets the escape key to call the pause menu.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                resume();
            }
            else
            {
                Pause();
            }
        }
    }

    //Disables the pause menu UI , sets time back to normal , game is in play. 
    public void resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    //Enables the pause menu UI , freezes the time , game is paused. 
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    //Sets time back to normal , loads the start scene.
    public void loadStartMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene");
    }

    //Exits the game.
    public void quitGame()
    {
        Application.Quit();
        Debug.Log("QUIT!");
    }
}
