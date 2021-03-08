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
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }


    public Text playerDiamondCountText;
    public Image selectionImage;
    public Text HUDDiamondCountText;
    public Image[] healthBar;

    public Text FlameSwordText;
    public Text FlameSwordPrice;

    public Text BootsOfFlightText;
    public Text BootsOfFlightPrice;

    public Text KeyToCastleText;
    public Text KeyToCastlePrice;

    public Text Item_Description;

    public GameObject menuPanel;
    private bool isMenuOpen;
    private Player player;

    private Color textAlpha;

    public void OpenShop(int diamondCount)
    {
        playerDiamondCountText.text = "" + diamondCount + " D";
        textAlpha = FlameSwordText.color;
        textAlpha.a = 0.5f;
        if (GameManager.Instance.HasFlameSword == true)
        {
            FlameSwordText.color = textAlpha;
            FlameSwordPrice.color = textAlpha;
        }
        if (GameManager.Instance.HasBootsOfFlight == true)
        {
            BootsOfFlightText.color = textAlpha;
            BootsOfFlightPrice.color = textAlpha;
        }
        if (GameManager.Instance.HasKeyToCastle == true)
        {
            KeyToCastleText.color = textAlpha;
            KeyToCastlePrice.color = textAlpha;
        }

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
        for (int i = 0; i <= livesRemaining; i++)
        {
            if (i == livesRemaining)
            {
                healthBar[i].enabled = false;
            }
        }
    }
    public void OpenCloseMenu()
    {
        if (isMenuOpen == false)
        {
            menuPanel.SetActive(true);
            isMenuOpen = true;
        }
        else if (isMenuOpen == true)
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
