using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Buy : MonoBehaviour
{
    public string myName;
    public int myNum;
    public int price;

    private void Update()
    {
        if (myName == "oxygen")
        {
            GetComponent<Button>().interactable = GameManager.instance.shopOxygen + 1 == myNum;
        }
        if (myName == "backpack")
        {
            GetComponent<Button>().interactable = GameManager.instance.shopBackpack + 1 == myNum;
        }
    }

    public void BuyOxygen()
    {
        if (GameManager.instance.money >= price)
        {
            GameManager.instance.money -= price;
            GameManager.instance.shopOxygen++;
        }
    }

    public void BuyBackpack()
    {
        if (GameManager.instance.money >= price)
        {
            GameManager.instance.money -= price;
            GameManager.instance.shopBackpack++;
            GameManager.instance.backpack += 2;
        }
    }
}
