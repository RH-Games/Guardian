using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    //buttons for Ui menu.
    public GameObject pauseMenu;
    public GameObject controls;
    public GameObject creditMenu;
    private static bool isPaused;
    private bool isOnCredits = false;
    


    private void Start()
    {
       // pauseMenu.SetActive(false);
    }

    private void Update()
    {
        //checks for the button press and if the game is paused or not
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if (isPaused)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene"); ; //starts the next scene in load order
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        print("quit button pressed");
        Application.Quit();
    }

    public void pauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;// stops the game
     


    }

    public void resumeGame()
    {
        isPaused=false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        
    }

    public void Credit(bool ToggleOn)
    {
       
        if (ToggleOn)
        {
            controls.SetActive(false);
            creditMenu.SetActive(true);
        }
        else
        {
            controls.SetActive(true);
            creditMenu.SetActive(false);
        }
         isOnCredits = ToggleOn;
    }
    


    public void mainmenuGame()
    {
        //Time.timeScale = 1f;
        SceneManager.LoadScene("Menu"); //loads the main menu
    }

    public void RetartGameLvl()
    {
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        Time.timeScale = 1f;
    }


}
