using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    public int myNum;

    private void Update()
    {
         GetComponent<Button>().interactable = GameManager.instance.canLevel >= myNum;
    }
}
