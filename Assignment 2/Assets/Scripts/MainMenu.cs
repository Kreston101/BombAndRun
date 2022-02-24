using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button playButton, tutorialButton, exitButton;
    public GameObject tutorialPanel;

    void Start()
    {
        //add listeners to buttons
        playButton.onClick.AddListener(playGame);
        tutorialButton.onClick.AddListener(openTutorial);
        exitButton.onClick.AddListener(exitMenu);
    }

    void playGame()
    {
        //loads the first level
        SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
    }

    void openTutorial()
    {
        //opens the turorial window
        Debug.Log("opening the window");
        tutorialPanel.SetActive(true);
    }

    void exitMenu()
    {
        //closes the tutorial menu
        Debug.Log("exited menu");
        tutorialPanel.SetActive(false);
    }
}
