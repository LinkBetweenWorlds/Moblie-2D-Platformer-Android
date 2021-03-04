using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UI Manager is Null");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }


    public Text playerDiamondCountText;
    public Image selectionImage;
    public Text HUDDiamondCountText;
    public Image[] healthBar;

    public void OpenShop(int diamondCount)
    {
        playerDiamondCountText.text = "" + diamondCount + " D";
    }

    public void UpdateShopSelection(int yPos)
    {
        selectionImage.rectTransform.anchoredPosition = new Vector2(selectionImage.rectTransform.anchoredPosition.x, yPos);
    }

    public void UpdateDiamondCount(int count)
    {
        HUDDiamondCountText.text = "" + count + " D";
    }

    public void UpdateLives(int livesRemaining)
    {
        for(int i = 0; i <= livesRemaining; i++)
        {
            if(i == livesRemaining)
            {
                healthBar[i].enabled = false;
            }
        }
    }

}
