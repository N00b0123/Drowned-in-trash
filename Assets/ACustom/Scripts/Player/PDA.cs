using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PDA : MonoBehaviour
{


    public static bool isOpenPDA = false;
    [SerializeField] GameObject pdaUI;
    [SerializeField] GameObject gameplayUI;
    [SerializeField] GameObject homePDA;
    [SerializeField] GameObject realHome;
    [SerializeField] GameObject whatIs;
    [SerializeField] GameObject whyHasSoMany;
    [SerializeField] GameObject whyIsAProblem;
    [SerializeField] GameObject diseases1;
    [SerializeField] GameObject diseases2;
    [SerializeField] GameObject diseases3;
    [SerializeField] GameObject diseases4;
    [SerializeField] GameObject diseases5;
    [SerializeField] GameObject diseases6;

    [SerializeField] GameObject gamePDA;
    [SerializeField] GameObject gameHome;
    [SerializeField] GameObject objective;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject collectableUI;
    [SerializeField] GameObject life;


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

    #region RealPDA Functions

    public void OpenRealHome()
    {
        homePDA.SetActive(false);
        realHome.SetActive(true);
    }

    public void WhatIs()
    {
        realHome.SetActive(false);
        whatIs.SetActive(true);
    }

    public void WhyHasSoMany()
    {
        realHome.SetActive(false);
        whyHasSoMany.SetActive(true);
    }

    public void WhyIsAProblem()
    {
        realHome.SetActive(false);
        whyIsAProblem.SetActive(true);
    }

    public void BackToRealHome()
    {
        whatIs.SetActive(false);
        whyHasSoMany.SetActive(false);
        whyIsAProblem.SetActive(false);
        diseases1.SetActive(false);
        diseases2.SetActive(false);
        diseases3.SetActive(false);
        diseases4.SetActive(false);
        diseases5.SetActive(false);
        diseases6.SetActive(false);
        realHome.SetActive(true);
    }

    public void BackToHomePDA()
    {
        CloseGamePDA();
        CloseRealSubmenu();
        homePDA.SetActive(true);
    }

    public void OpenDiseasePage1()
    {
        diseases2.SetActive(false);
        realHome.SetActive(false);
        diseases1.SetActive(true);
    }

    public void OpenDiseasePage2()
    {
        diseases1.SetActive(false);
        diseases3.SetActive(false);
        diseases2.SetActive(true);
    }

    public void OpenDiseasePage3()
    {
        diseases2.SetActive(false);
        diseases4.SetActive(false);
        diseases3.SetActive(true);
    }

    public void OpenDiseasePage4()
    {
        diseases3.SetActive(false);
        diseases5.SetActive(false);
        diseases4.SetActive(true);
    }

    public void OpenDiseasePage5()
    {
        diseases4.SetActive(false);
        diseases6.SetActive(false);
        diseases5.SetActive(true);
    }

    public void OpenDiseasePage6()
    {
        diseases5.SetActive(false);
        diseases6.SetActive(true);
    }

    void CloseRealSubmenu()
    {
        realHome.SetActive(false);
        whatIs.SetActive(false);
        whyHasSoMany.SetActive(false);
        whyIsAProblem.SetActive(false);
        diseases1.SetActive(false);
        diseases2.SetActive(false);
        diseases3.SetActive(false);
        diseases4.SetActive(false);
        diseases5.SetActive(false);
        diseases6.SetActive(false);
    }

    #endregion

    void CloseGamePDA()
    {
        gameHome.SetActive(false);
        objective.SetActive(false);
        enemy.SetActive(false);
        collectableUI.SetActive(false);
        life.SetActive(false);
    }

    public void OpenGameHome()
    {
        homePDA.SetActive(false);
        gameHome.SetActive(true);
    }

    public void OpenObjective()
    {
        gameHome.SetActive(false);
        objective.SetActive(true);
    }
    public void OpenEnemy()
    {
        gameHome.SetActive(false);
        enemy.SetActive(true);
    }

    public void OpenCollectable()
    {
        gameHome.SetActive(false);
        collectableUI.SetActive(true);
        life.SetActive(false);
    }

    public void OpenLife()
    {
        collectableUI.SetActive(false);
        life.SetActive(true);
    }

    public void BackToGameHome()
    {
        collectableUI.SetActive(false);
        life.SetActive(false);
        enemy.SetActive(false);
        objective.SetActive(false);
        gameHome.SetActive(true);
    }

    void OpenPDA()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        gameplayUI.SetActive(false);
        pdaUI.SetActive(true);
        homePDA.SetActive(true);
        isOpenPDA = true;
    }

    void ClosePDA()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        pdaUI.SetActive(false);
        CloseRealSubmenu();
        gameplayUI.SetActive(true);
        isOpenPDA = false;
    }

}
