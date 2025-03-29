using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Open : MonoBehaviour
{
    public bool isKey;

    public int myNum;
    public List<PedalData> pedals = new();
    public List<GameObject> mushrooms = new();

    private bool isOpen;

    private void Start()
    {

        if (GameManager.instance.openChest.Contains(myNum))
        {
            isOpen = true;
            StageManager.instance.currentOpenChest.Add(myNum);
            GetComponent<Animator>().SetTrigger("Open");
        }
    }

    private void Update()
    {
        if (isOpen) return;
        bool open = true;

        foreach (var pedal in pedals)
        {
            if (!pedal.press)
                open = false;
        }
        foreach(var mushroom in mushrooms)
        {
            if (mushroom)
                open = false;
        }

        if (open)
        {
            if (isKey)
                GameManager.instance.canLevel += 1;

            isOpen = true;
            StageManager.instance.currentOpenChest.Add(myNum);
            GetComponent<Animator>().SetTrigger("Open");
        }
    }
}