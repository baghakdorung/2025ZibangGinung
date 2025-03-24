using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    // 플레이어
    public int backpack = 4;
    public int money = 0;

    public int shopOxygen = 0;
    public int shopBackpack = 0;

    // 스테이지
    public int currentLevel = 1;

    public float totalTime;
    public List<int> openChest = new();
    public List<int> openItem = new();

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}
