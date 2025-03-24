using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Player player;
    public StageManager stageManager;

    public int myNum;
    public string myName;
    public int myWeight;

    private void Start()
    {
        if (GameManager.instance.openItem.Contains(myNum))
        {
            stageManager.currentGetItem.Add(myNum);
            Destroy(transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.instance.backpack > player.inventory.Count)
        {
            stageManager.currentGetItem.Add(myNum);
            player.inventory.Add(myName);
            player.weight.Add(myWeight);
            Destroy(transform.parent.gameObject);
        }
    }
}
