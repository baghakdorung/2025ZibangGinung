using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Image[] slots;

    void Start()
    {
        for (int i = 0; i < GameManager.instance.backpack; i++)
        {
            slots[i].color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
