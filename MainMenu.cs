using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MainMenu : MonoBehaviour
{
    public void StartButton()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        if (!data.currentScene.Equals("Level_One"))
        {
            SceneManager.LoadScene(data.currentScene);
        }
        else
        {
            SceneManager.LoadScene("Level_One");
        }

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
