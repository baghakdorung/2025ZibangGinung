using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SelectedItem : MonoBehaviour
{
    public Player player;
    public GameObject road;

    public GameObject[] ItemLocation;
    public int SelectingItem = 9;
    public float maxItem = 4;


    void Start()
    {
        maxItem = GameManager.instance.backpack;
    }

    void Update()
    {
        SelectItem();
        UseItem();
    }

    void SelectItem()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectingItem = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectingItem = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectingItem = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectingItem = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectingItem = 4;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SelectingItem = 5;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SelectingItem = 6;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SelectingItem = 7;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SelectingItem = 8;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SelectingItem = 9;
        }

        if (SelectingItem < ItemLocation.Length && SelectingItem < maxItem)
        {
            GetComponent<Image>().enabled = true;
            transform.position = ItemLocation[SelectingItem].transform.position;
        }
        else
            GetComponent<Image>().enabled = false;
    }

    void UseItem()
    {
        if (Input.GetKeyDown(KeyCode.Space) &&
            SelectingItem < ItemLocation.Length &&
            SelectingItem < maxItem &&
            player.inventory.Count > SelectingItem &&
            player.inventory[SelectingItem] != null &&
            player.inventory[SelectingItem] != "Diamond"
        )
        {
            string item = player.inventory[SelectingItem];
            if (item == "Hp")
                player.hp += 60;
            else if (item == "Oxygen")
                player.oxygen += 60;
            else if (item == "Compass")
                road.SetActive(true);
            else if (item == "Speed")
                player.moveSpeed += 0.5f;
            else if (item == "Speeed")
                player.moveSpeed += 1.0f;
            else if (item == "Invisible")
                player.invisibleTime = 30.0f;

            player.inventory.RemoveAt(SelectingItem);
        }
    }
}
