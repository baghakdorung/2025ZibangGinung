using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : Singleton<StageManager>
{
    // ��
    public string mainScene;

    // ������Ʈ
    public GameObject player;
    public Camera mainCamera;

    // ���� ��ġ
    public List<Vector3> spawnPoint = new();

    // �ð�
    public float stageTime = 0.0f;
    private int stageLevel = 0;

    // �� ����
    public List<int> currentOpenChest = new();

    private void Start()
    {
        stageLevel = GameManager.instance.currentLevel;
        Vector3 originalPosition = spawnPoint[stageLevel - 1];
        Vector3 cameraPosition = originalPosition;
        cameraPosition.y += 7;
        cameraPosition.z += 1.3f;
        Vector3 ExitPosition = originalPosition;
        ExitPosition.z += -1.5f;

        player.transform.position = originalPosition;
        mainCamera.transform.position = cameraPosition;
        transform.position = ExitPosition;

        currentOpenChest = GameManager.instance.openChest;
    }

    private void Update()
    {
        stageTime += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Exit());
    }

    public IEnumerator Exit()
    {
        Fade.instance.FadeOut();
        player.GetComponent<Player>().stopMove = true;
        yield return new WaitForSeconds(0.5f);

        GameManager.instance.totalTime += stageTime;
        GameManager.instance.openChest = currentOpenChest;
        SceneManager.LoadScene(mainScene);
    }
}
