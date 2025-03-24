using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public Player player;

    // 바
    public Image hpFill;
    public Image oxygenFill;

    // 가방
    public Image backpackFill;
    public Image backpackOverFill;
    public Text weight;

    // 아이템 목록
    public string[] itemName;
    public Sprite[] itemImage;

    private void Update()
    {
        UpdateHPFill();
        UpdateOxygenFill();
        UpdateInventory();
    }

    private void UpdateHPFill()
    {

        hpFill.fillAmount = player.hp / player.maxHP;
    }

    private void UpdateOxygenFill()
    {
        oxygenFill.fillAmount = player.oxygen / player.maxOxygen;
    }

    private void UpdateInventory()
    {
        backpackFill.fillAmount = player.weight.Sum() / 100.0f;
        backpackOverFill.fillAmount = player.weight.Sum() / 100.0f - 1;
        weight.text = $"{player.weight.Sum()} / 100";
    }
}
