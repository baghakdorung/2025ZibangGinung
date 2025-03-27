using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    private void Update()
    {
        GetComponent<Text>().text = Mathf.Min(GameManager.instance.money, 999).ToString();

        if (Input.GetKeyDown(KeyCode.F2))
        {
            GameManager.instance.money = 99999;
        }
    }
}
