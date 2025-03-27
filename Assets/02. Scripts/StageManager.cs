using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
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

    // �ر�
    public List<int> currentOpenChest = new();
    public List<int> currentGetItem = new();

    private void Awake()
    {
        // Ŀ��
        Cursor.visible = false;

        // ����
        stageLevel = GameManager.instance.currentLevel;

        // ����
        currentOpenChest = GameManager.instance.openChest.ToList();
        currentGetItem = GameManager.instance.openItem.ToList();
    }

    private void Start()
    {
        // ���� ����
        Vector3 originalPosition = spawnPoint[stageLevel - 1];
        Vector3 cameraPosition = originalPosition;
        cameraPosition.y += 7;
        cameraPosition.z += 1.3f;
        Vector3 ExitPosition = originalPosition;
        ExitPosition.z += -1.5f;

        // ���� �̵�
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
        // ���߱�
        Fade.instance.FadeOut();
        player.GetComponent<Player>().stopMove = true;
        yield return new WaitForSeconds(0.5f);

        // ������ ����
        GameManager.instance.totalTime += stageTime;
        GameManager.instance.openChest = currentOpenChest.ToList();
        GameManager.instance.openItem = currentGetItem.ToList();
        GameManager.instance.money += player.GetComponent<Player>().weight.Sum();

        // �ε�
        Cursor.visible = true;
        Fade.instance.FadeIn();
        SceneManager.LoadScene(mainScene);
    }
}
