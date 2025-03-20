using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Player player;

    public Image hpFill;
    public Image oxygenFill;

    private void Update()
    {
        UpdateHPFill();
        UpdateOxygenFill();
    }

    private void UpdateHPFill()
    {

        hpFill.fillAmount = player.hp / player.maxHP;
    }

    private void UpdateOxygenFill()
    {
        oxygenFill.fillAmount = player.oxygen / player.maxOxygen;
    }
}
