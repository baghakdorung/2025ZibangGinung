using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // 플레이어
    public int backpack = 4;
    public int money = 0;

    public int shopOxygen = 0;
    public int shopBackpack = 0;

    // 스테이지
    public int canLevel = 1;
    public int currentLevel = 1;

    public float totalTime;
    public List<int> openChest = new();
    public List<int> openItem = new();

    private bool isPaused = false;

    public bool clear = false;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }
}
