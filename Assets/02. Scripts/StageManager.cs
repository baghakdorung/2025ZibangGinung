using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : Singleton<StageManager>
{
    public float stageTime = 0.0f;
    public string sceneName;
    public List<int> currentOpenChest = new();

    private void Start()
    {
        currentOpenChest = GameManager.instance.openChest;
    }

    private void Update()
    {
        stageTime += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Exit();
    }

    public void Exit()
    {
        GameManager.instance.totalTime += stageTime;
        GameManager.instance.openChest = currentOpenChest;
        SceneManager.LoadScene(sceneName);
    }
}
