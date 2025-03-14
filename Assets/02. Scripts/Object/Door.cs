using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public List<PedalData> pedals = new();

    private bool isOpen;

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
            GetComponent<Animator>().SetTrigger("Open");
        }
    }
}