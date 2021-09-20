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

    private void Update()
    {
        if (!(SceneManager.GetActiveScene().name == "MainMenu"))
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
            {
                if (isPaused)
                    Resume();
                else
                    Pause();
            }
        }
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        gameplayUI.SetActive(false);
        isPaused = true;
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
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
        SceneManager.LoadScene(1);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(2);
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
        Debug.Log("Fui eu quem fez");
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
            Debug.Log("Faleceu");
            Cursor.lockState = CursorLockMode.None;
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
