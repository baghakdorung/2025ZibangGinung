using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    private void Update()
    {
        GetComponent<Text>().text = GameManager.instance.money.ToString();
    }
}
