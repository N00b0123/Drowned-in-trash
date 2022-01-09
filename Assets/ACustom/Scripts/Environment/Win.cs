using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject gameplay;
    [SerializeField] GameObject pda;

    private void OnTriggerEnter(Collider other)
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        pda.SetActive(false);
        gameplay.SetActive(false);
        winScreen.SetActive(true);
    }
}
