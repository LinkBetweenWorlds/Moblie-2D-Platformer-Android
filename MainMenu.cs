using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("Level_One");
    }
    public void MenuButton()
    {
        SceneManager.LoadScene("Settings");
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
