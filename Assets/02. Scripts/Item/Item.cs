using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private Player player;

    public int myNum;
    public string myName;
    public int myWeight;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        if (GameManager.instance.openItem.Contains(myNum))
        {
            StageManager.instance.currentGetItem.Add(myNum);
            Destroy(transform.gameObject);
        }

        if (myName == "Random")
        {
            switch (Random.Range(1, 7))
            {
                case 1:
                    myName = "Hp";
                    break;
                case 2:
                    myName = "Oxygen";
                    break;
                case 3:
                    myName = "Compass";
                    break;
                case 4:
                    myName = "Speed";
                    break;
                case 5:
                    myName = "Speeed";
                    break;
                case 6:
                    myName = "Invisible";
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.instance.backpack > player.inventory.Count)
        {
            StageManager.instance.currentGetItem.Add(myNum);
            player.inventory.Add(myName);
            player.weight.Add(myWeight);
            Destroy(transform.gameObject);
        }
    }
}
