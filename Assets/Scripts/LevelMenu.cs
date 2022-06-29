using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    //Loads "Level 1" scene when the level 1 button is pressed.
   public void loadLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }

    //Loads "Level 2" scene when the level 2 button is pressed.
    public void loadLevel2()
    {
        SceneManager.LoadScene("Level 2");
    }

    //Loads "Level 3" scene when the level 3 button is pressed.
    public void loadLevel3()
    {
        SceneManager.LoadScene("Level 3");
    }
}
