using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool isGameOver = false;
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject gameplayUI;
    public GameObject gameOverUI;
    public GameObject creditsUI;
    public GameObject mainMenuUI;

    private void Awake()
    {
        AudioManager.Initialize();
    }

    private void Update()
    {
        if (!(SceneManager.GetActiveScene().name == "MainMenu"))
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver && !PDA.isOpenPDA)
            {
                if (isPaused)
                {
                    Resume();
                } 
                else
                {
                    Pause();
                }   
            }
        }
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        gameplayUI.SetActive(false);
        isPaused = true;
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        gameplayUI.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
        isGameOver = false;
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void Options()
    {
        Debug.Log("Go to Options, future implementation");
    }

    public void Credits()
    {
        mainMenuUI.SetActive(false);
        creditsUI.SetActive(true);
    }
    
    public void BackToMenuUI()
    {
        creditsUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        isGameOver = false;
    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            gameplayUI.SetActive(false);
            gameOverUI.SetActive(true);
            Invoke("DelayGameOver", 1.2f);
        }
    }

    void DelayGameOver()
    {
        Time.timeScale = 0f;
    }

}
