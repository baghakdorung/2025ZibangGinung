using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Player player;

    public Image hpFill;

    private void Update()
    {
        UpdateHPFill();
    }

    private void UpdateHPFill()
    {

        hpFill.fillAmount = player.hp / player.maxHP;
    }
}
