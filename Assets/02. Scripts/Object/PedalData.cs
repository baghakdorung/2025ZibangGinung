using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedalData : MonoBehaviour
{
    public bool press;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Object"))
            press = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Object"))
            press = false;
    }
}