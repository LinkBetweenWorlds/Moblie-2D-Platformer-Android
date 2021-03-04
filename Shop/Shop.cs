using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;
    public int currentSelectedItem;
    public int currentItemCost;

    private Player player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player = other.GetComponent<Player>();

            if (player != null)
            {
                UIManager.Instance.OpenShop(player.diamonds);
            }

            shopPanel.SetActive(true);
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
                UIManager.Instance.UpdateShopSelection(60);
                currentSelectedItem = 0;
                currentItemCost = 200;
                break;

            case 1: //boots
                UIManager.Instance.UpdateShopSelection(-40);
                currentSelectedItem = 1;
                currentItemCost = 300;
                break;

            case 2: //key
                UIManager.Instance.UpdateShopSelection(-140);
                currentSelectedItem = 2;
                currentItemCost = 100;
                break;
        }
    }

    public void BuyItem()
    {
        if(player.diamonds >= currentItemCost)
        {
            if (currentSelectedItem == 2)
            {
                GameManager.Instance.HasKeyToCastle = true;
            }

            player.diamonds -= currentItemCost;
            Debug.Log("Purchased : " + currentSelectedItem);
            Debug.Log(player.diamonds);
            shopPanel.SetActive(false);
        }
        else
        {
            Debug.Log("Poor person");
            shopPanel.SetActive(false);
        }
    }
}
