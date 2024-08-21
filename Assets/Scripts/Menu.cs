using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject settingMenu;

    [SerializeField] GameObject MainMenu;

    public void MenuLoad(string lvl)
    {
        SceneManager.LoadScene(lvl);
    }

    public void SettingMenu()
    {
        MainMenu.SetActive(false);
        settingMenu.SetActive(true);
    }

    public void ExitSettingMenu()
    {
        settingMenu.SetActive(false);
        MainMenu.SetActive(true);
    }


    public void QuitGame() 
    {
        Application.Quit();
    }
}
