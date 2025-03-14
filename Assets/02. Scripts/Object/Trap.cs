using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private bool state = false;
    
    private void Start()
    {
        InvokeRepeating(nameof(Show), 0f, 2f);
    }

    private void Show()
    {
        state = !state;
        GetComponent<Animator>().SetBool("show", state);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && state)
        {
            other.GetComponent<Player>().SetDamage(5);
        }
    }
}
