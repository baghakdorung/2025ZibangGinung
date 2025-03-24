using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : Singleton<Ranking>
{
    public List<RankData> rankData = new();
    public List<Transform> rankUi = new();

    public InputField nameInput;
    public CanvasGroup inputGroup;

    protected override void Awake()
    {
        // PlayerPrefs.DeleteAll();
        base.Awake();
    }

    private void Start()
    {
        Load();
        UpdateUI();
    }

    public void Register()
    {
        string name = nameInput.text;
        int time = 100;

        rankData.Add(new RankData(name, time));

        inputGroup.interactable = false;
        Save();
    }

    public void Save()
    {
        Sort();

        for (int i = 0; i < rankData.Count; i++)
        {
            PlayerPrefs.SetString($"RankName{i}", rankData[i].name);
            PlayerPrefs.SetInt($"RankTime{i}", rankData[i].time);
        }

        UpdateUI();
    }

    public void Load()
    {
        rankData.Clear();

        for (int i = 0; i < 5; i++)
        {
            string name = PlayerPrefs.GetString($"RankName{i}", "------");
            int time = PlayerPrefs.GetInt($"RankTime{i}", 5999);

            rankData.Add(new RankData(name, time));
        }
    }

    public void Sort()
    {
        rankData = rankData.OrderBy(a => a.time).ToList();
        rankData = rankData.GetRange(0, Mathf.Min(5, rankData.Count));
    }

    public void UpdateUI()
    {
        for (int i = 0; i < 5; i++)
        {
            Text rank = rankUi[i].GetChild(0).GetComponent<Text>();
            Text name = rankUi[i].GetChild(1).GetComponent<Text>();
            Text time = rankUi[i].GetChild(2).GetComponent<Text>();

            rank.text = $"{i + 1}";
            name.text = $"{rankData[i].name}";

            int mm = rankData[i].time / 60;
            int ss = rankData[i].time % 60;

            time.text = $"{mm:D2} : {ss:D2}";
        }
    }
}

[System.Serializable]
public class RankData
{
    public string name;
    public int time;

    public RankData(string name, int item)
    {
        this.name = name;
        this.time = item;
    }
}
