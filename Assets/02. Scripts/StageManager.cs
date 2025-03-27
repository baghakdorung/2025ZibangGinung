using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    // 해금
    public List<int> currentOpenChest = new();
    public List<int> currentGetItem = new();

    private void Awake()
    {
        // 커서
        Cursor.visible = false;

        // 레벨
        stageLevel = GameManager.instance.currentLevel;

        // 상자
        currentOpenChest = GameManager.instance.openChest.ToList();
        currentGetItem = GameManager.instance.openItem.ToList();
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
        if (Input.GetKeyDown(KeyCode.F3))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            GameManager.instance.currentLevel++;
            if (GameManager.instance.currentLevel > 5)
                GameManager.instance.currentLevel = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Exit());
    }

    public IEnumerator Exit()
    {
        // 멈추기
        Fade.instance.FadeOut();
        player.GetComponent<Player>().stopMove = true;
        yield return new WaitForSeconds(0.5f);

        // 데이터 저장
        GameManager.instance.totalTime += stageTime;
        GameManager.instance.openChest = currentOpenChest.ToList();
        GameManager.instance.openItem = currentGetItem.ToList();
        GameManager.instance.money += player.GetComponent<Player>().weight.Sum();

        // 로드
        Cursor.visible = true;
        Fade.instance.FadeIn();
        SceneManager.LoadScene(mainScene);
    }
}
