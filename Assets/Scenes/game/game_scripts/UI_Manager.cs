using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public GameObject settings_menu;
    public GameObject MenuUI;
    public GameObject FoodMenuUI;
    public GameObject FoodMenu_button;
    public GameObject Menu_button;
    public GameObject Cursor;
    public GameObject Wanna_save;
    public Stats statscript;
    
    public void OnMenuPress()
    {
        MenuUI.SetActive(true);
        Menu_button.SetActive(false);
        FoodMenu_button.SetActive(false);
        Cursor.SetActive(false);
    }public void OnFoodPress()
    {
        FoodMenuUI.SetActive(true);
        Menu_button.SetActive(false);
        FoodMenu_button.SetActive(false);
        Cursor.SetActive(false);
    }
    public void OnResumePress(int type)
    {
        if(type == 0)
        {
            Menu_button.SetActive(true);
            FoodMenu_button.SetActive(true);
            Cursor.SetActive(true);
        }
        MenuUI.SetActive(false);
        FoodMenuUI.SetActive(false);
        Wanna_save.SetActive(false);
        settings_menu.SetActive(false);
    }
    public void OnSettingsPress()
    {
        Wanna_save.SetActive(false);
        settings_menu.SetActive(true);
    }
    public void OnBackToMainMenuPress()
    {
        Wanna_save.SetActive(true);
        settings_menu.SetActive(false);
    }
    public void OnNOBackToMainMenuPress()
    {
        SceneManager.LoadScene(0);
    }
    public void OnYESBackToMainMenuPress()
    {
        statscript.SaveStats();
        SceneManager.LoadScene(0);
        
    }

}
