using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowBlock : MonoBehaviour
{
    public bool touch;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !touch)
        {
            touch = true;
            other.transform.position += new Vector3(0, 0.1f, 0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            touch = false;
    }
}
