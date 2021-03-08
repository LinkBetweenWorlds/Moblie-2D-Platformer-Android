using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject menuPanel;
    private bool isMenuOpen;
    private Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    public void OpenCloseMenu()
    {
        if (isMenuOpen == false)
        {
            menuPanel.SetActive(true);
            isMenuOpen = true;
        }
        else if(isMenuOpen == true)
        {
            menuPanel.SetActive(false);
            isMenuOpen = false;
        }

    }

    public void SaveGame()
    {
        SaveSystem.SavePlayer(player);
    }

    public void QuitGame()
    {
        SaveSystem.SavePlayer(player);
        Application.Quit();
    }
}
