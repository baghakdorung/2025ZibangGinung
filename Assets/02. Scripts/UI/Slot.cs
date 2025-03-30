using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Player player;
    public int myNum;

    public List<string> itemName;
    public List<Sprite> itemImage;

    public string myItemName;

    private void Update()
    {
        if (player.inventory.Count > myNum && player.inventory[myNum] != null)
        {
            // n번째 템을
            string currentItem = player.inventory[myNum];

            // 찾아서
            int itemIndex = itemName.IndexOf(currentItem);

            // 이미지 적용
            GetComponent<Image>().enabled = true;
            GetComponent<Image>().sprite = itemImage[itemIndex];
        }
        else
        {
            GetComponent<Image>().enabled = false;
        }
    }
}
