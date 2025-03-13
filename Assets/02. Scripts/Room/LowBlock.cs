using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowBlock : MonoBehaviour
{
    public bool touch;

    private void OnCollderEnter(Collider other)
    {
        if (other.tag == "Player" && touch)
        {
            touch = true;
            other.transform.position += new Vector3(0, 0.2f, 0);
        }
    }

    private void OnCollderExit(Collider other)
    {
        if (other.tag == "Player")
            touch = false;
    }

    private void Onco
}
