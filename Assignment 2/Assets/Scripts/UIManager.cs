using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Button pauseButton, restartButton;
    public string level;

    private bool levelPaused;

    // Start is called before the first frame update
    void Start()
    {
        //add listeners to buttons
        restartButton.onClick.AddListener(restartLevel);
        pauseButton.onClick.AddListener(pauseLevel);
    }

    //restart the level by reloading the screen
    ////sets paused to false in the even that the player has paused before restarting the level
    private void restartLevel()
    {
        SceneManager.LoadScene(level, LoadSceneMode.Single);
        levelPaused = false;
        Time.timeScale = 1;
    }

    //pauses the level
    private void pauseLevel()
    {
        //if level is not paused, set levelPaused to true and set the timescale to 0
        if (levelPaused == false)
        {
            Time.timeScale = 0;
            levelPaused = true;
        }
        //if level is paused, set levelPaused to false and set the timescale to 1
        else
        {
            Time.timeScale = 1;
            levelPaused = false;
        }
    }
}
