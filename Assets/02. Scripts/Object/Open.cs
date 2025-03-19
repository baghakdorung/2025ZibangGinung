using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class Open : MonoBehaviour
{
    public int myNum;
    public List<PedalData> pedals = new();

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

        if (open)
        {
            isOpen = true;
            StageManager.instance.currentOpenChest.Add(myNum);
            GetComponent<Animator>().SetTrigger("Open");
        }
    }
}