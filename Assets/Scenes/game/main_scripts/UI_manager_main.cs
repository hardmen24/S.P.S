using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_manager_main : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Info_menu;
    public GameObject Play_menu;
    public GameObject Settings_menu;
    public TMP_InputField nameInput;
    public Stats stats_script;
    
    public void OnInfoPress()
    {
        Info_menu.SetActive(true);
        Menu.SetActive(false);
        
    }
    public void OnPlayPress()
    {
        Play_menu.SetActive(true);
        Menu.SetActive(false);
        
    }
    public void OnSettingsPress()
    {
        Settings_menu.SetActive(true);
        Menu.SetActive(false);
    }
    public void OnBackPress()
    {
        
        Menu.SetActive(true);
        Info_menu.SetActive(false);
        Play_menu.SetActive(false);
        Settings_menu.SetActive(false);
    }
    public void OnQuitPress()
    {
        Application.Quit();
    }

    public void OnNewGamePress()
    {
        if (string.IsNullOrEmpty(nameInput.text))
        {
            Debug.LogWarning("Name input is empty. Please enter a name for your slime.");
            return;
        }
    player_data.current = new player_data
    {
        slime_name = nameInput.text,
        hunger = 80f,
        sleepiness = 80f,
        happiness = 80f,
        health = 100f,
        age = 0f
    };
     string filename = nameInput.text;
    Save_game.SaveData(filename, player_data.current);
    SceneManager.LoadScene(1);
   
    }

    public void OnLoadPress(string saveName)
    {
    player_data.current = Save_game.LoadData<player_data>(saveName);
    SceneManager.LoadScene(1);
    }

}
