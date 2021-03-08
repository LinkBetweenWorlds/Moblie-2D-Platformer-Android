using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;
    public int currentSelectedItem;
    public int currentItemCost;

    private Player player;
    private string FlameSwordItemDescription = "This adds a fire aspect to your sword allowing you to double your attack damage.";
    private string BootsOfFlightItemDescription = "These boots increase both your jump height, and allow you to run faster.";
    private string KeyToCastleItemDescription = "These are the keys to the castle. You will need them to get into the castle.";
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player = other.GetComponent<Player>();

            if (player != null)
            {
                UIManager.Instance.OpenShop(player.diamonds);
            }
            if (GameManager.Instance.HasFlameSword == true && GameManager.Instance.HasBootsOfFlight == true && GameManager.Instance.HasKeyToCastle == true)
            {
                return;
            }
            else
            {
                shopPanel.SetActive(true);
                SelectItem(0);
            }

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            shopPanel.SetActive(false);
        }
    }

    public void SelectItem(int item)
    {
        //0 = flame sword
        //1 = boots of flight
        //2 = key to castle
        Debug.Log("SelectItem : " + item);

        switch (item)
        {
            case 0: //flame sword
                if (GameManager.Instance.HasFlameSword == false)
                {
                    UIManager.Instance.UpdateShopSelection(60);
                    UIManager.Instance.Item_Description.text = FlameSwordItemDescription;
                    currentSelectedItem = 0;
                    currentItemCost = 200;
                }
                else
                {
                    SelectItem(item + 1);
                }
                break;

            case 1: //boots
                if (GameManager.Instance.HasBootsOfFlight == false)
                {
                    UIManager.Instance.UpdateShopSelection(-40);
                    UIManager.Instance.Item_Description.text = BootsOfFlightItemDescription;
                    currentSelectedItem = 1;
                    currentItemCost = 300;
                }
                else
                {
                    SelectItem(item + 1);
                }
                break;

            case 2: //key
                if (GameManager.Instance.HasKeyToCastle == false)
                {
                    UIManager.Instance.UpdateShopSelection(-140);
                    UIManager.Instance.Item_Description.text = KeyToCastleItemDescription;
                    currentSelectedItem = 2;
                    currentItemCost = 100;
                }
                else
                {
                    SelectItem(item + 1);
                }
                break;
            case 3:
                if(GameManager.Instance.HasFlameSword == true && GameManager.Instance.HasBootsOfFlight == true && GameManager.Instance.HasKeyToCastle == true)
                {
                    shopPanel.SetActive(false);
                }
                else
                {
                    SelectItem(0);
                }
                break;

        }
    }

    public void BuyItem()
    {
        if (player.diamonds >= currentItemCost)
        {
            switch (currentSelectedItem)
            {
                case 0:
                    GameManager.Instance.HasFlameSword = true;
                    break;
                case 1:
                    GameManager.Instance.HasBootsOfFlight = true;
                    player.jumpforce = 12.0f;
                    player.playerSpeed = 10;
                    break;
                case 2:
                    GameManager.Instance.HasKeyToCastle = true;
                    break;
            }

            player.diamonds -= currentItemCost;
            Debug.Log("Purchased : " + currentSelectedItem);
            Debug.Log(player.diamonds);
            shopPanel.SetActive(false);
            UIManager.Instance.UpdateDiamondCount(player.diamonds);
            SaveSystem.SavePlayer(player);
        }
        else
        {
            Debug.Log("Poor person");
            shopPanel.SetActive(false);
        }
    }
}
