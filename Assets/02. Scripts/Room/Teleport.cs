using System.Collections;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    // ī�޶�
    private Camera mainCamera;

    // �� ����
    private GameObject[] objects;
    private GameObject[] enemies;
    private Vector3[] objectPositions;
    private Vector3[] enemyPositions;

    // �Ÿ�
    public float targetDistance;

    private void Start()
    {
        // ī�޶�
        mainCamera = Camera.main;


        // ������Ʈ ��ǥ ����
        objects = GameObject.FindGameObjectsWithTag("Object");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        objectPositions = new Vector3[objects.Length];
        enemyPositions = new Vector3[enemies.Length];

        for (int i = 0; i < objects.Length; i++)
        {
            objectPositions[i] = objects[i].transform.position;
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            enemyPositions[i] = enemies[i].transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ResetRoom(other));
        }
    }

    private IEnumerator ResetRoom(Collider player)
    {
        // 1. �÷��̾� �̵� ����
        player.GetComponent<Player>().stopMove = true;

        // 2. ���̵� �ƿ�
        Fade.instance.FadeOut();
        yield return new WaitForSeconds(0.25f);

        // 3. �� ����
        for (int i = 0; i < objects.Length; i++)
        {
           objects[i].transform.position = objectPositions[i];
        }
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].transform.position = enemyPositions[i];
        }

        // 4. �÷��̾� �̵�
        Vector3 direction = transform.forward;
        Vector3 playerTpPosition = player.transform.position + direction * targetDistance;
        player.transform.position = playerTpPosition;

        // 5. ī�޶� �̵�
        Vector3 cameraPosition = mainCamera.transform.position;
        cameraPosition.x += direction.x * 20;
        cameraPosition.z += direction.z * 20;
        mainCamera.transform.position = cameraPosition;

        // 6. ���̵� ��
        Fade.instance.FadeIn();
        yield return new WaitForSeconds(0.25f);

        // 7. �÷��̾� �̵� ����
        player.GetComponent<Player>().stopMove = false;
    }
}
