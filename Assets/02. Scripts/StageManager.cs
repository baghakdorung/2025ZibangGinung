using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    // 씬
    public string mainScene;

    // 오브젝트
    public GameObject player;
    public Camera mainCamera;

    // 스폰 위치
    public List<Vector3> spawnPoint = new();

    // 시간
    public float stageTime = 0.0f;
    private int stageLevel = 0;

    // 연 상자
    public List<int> currentOpenChest = new();

    private void Awake()
    {
        // 커서
        Cursor.visible = false;

        // 레벨
        stageLevel = GameManager.instance.currentLevel;

        // 상자
        currentOpenChest = GameManager.instance.openChest;
    }

    private void Start()
    {
        // 스폰 설정
        Vector3 originalPosition = spawnPoint[stageLevel - 1];
        Vector3 cameraPosition = originalPosition;
        cameraPosition.y += 7;
        cameraPosition.z += 1.3f;
        Vector3 ExitPosition = originalPosition;
        ExitPosition.z += -1.5f;

        // 스폰 이동
        player.transform.position = originalPosition;
        mainCamera.transform.position = cameraPosition;
        transform.position = ExitPosition;
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

        Cursor.visible = true;
        Fade.instance.FadeIn();
        SceneManager.LoadScene(mainScene);
    }
}
