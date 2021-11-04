using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PDA : MonoBehaviour
{
    public static bool isOpenPDA = false;
    [SerializeField] GameObject pdaUI;
    [SerializeField] GameObject gameplayUI;
    [SerializeField] TextMeshProUGUI pdaText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !GameManager.isGameOver && !GameManager.isPaused)
        {
            if (isOpenPDA)
            {
                ClosePDA();
            }
            else
            {
                OpenPDA();
            }
        }
    }

    void OpenPDA()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        gameplayUI.SetActive(false);
        pdaUI.SetActive(true);
        isOpenPDA = true;
    }

    void ClosePDA()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        pdaUI.SetActive(false);
        gameplayUI.SetActive(true);
        isOpenPDA = false;
    }
}
