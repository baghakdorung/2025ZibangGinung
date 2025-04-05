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

    public float shopBackpack;

    private void Start()
    {
        shopBackpack = 100 + GameManager.instance.shopBackpack * 150;
    }

    private void Update()
    {
        UpdateHPFill();
        UpdateOxygenFill();
        UpdateInventory();
        Cheat();
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
        backpackFill.fillAmount = player.weight.Sum() / shopBackpack;
        backpackOverFill.fillAmount = player.weight.Sum() / shopBackpack - 1;
        weight.text = $"{player.weight.Sum()} / {(int)shopBackpack}";
    }

    private void Cheat()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            player.hp = player.maxHP;
            player.oxygen = player.maxOxygen;
        }
    }
}
