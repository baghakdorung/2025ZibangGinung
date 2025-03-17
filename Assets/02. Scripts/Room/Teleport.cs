using System.Collections;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    // ī�޶�
    private Camera mainCamera;

    // �� ����
    private GameObject[] objects;
    private Vector3[] objectPositions;

    // ���̵�
    private GameObject fade;

    // �Ÿ�
    public float targetDistance;

    private void Start()
    {
        // ī�޶�
        mainCamera = Camera.main;


        // ������Ʈ ��ǥ ����
        objects = GameObject.FindGameObjectsWithTag("Object");
        objectPositions = new Vector3[objects.Length];

        for (int i = 0; i < objects.Length; i++)
        {
            objectPositions[i] = objects[i].transform.position;
        }


        // fade
        fade = GameObject.FindGameObjectWithTag("Fade");
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
        fade.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(0.25f);

        // 3. �� ����
        for (int i = 0; i < objects.Length; i++)
        {
           objects[i].transform.position = objectPositions[i];
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
        fade.GetComponent<Animator>().SetTrigger("FadeIn");
        yield return new WaitForSeconds(0.25f);

        // 7. �÷��̾� �̵� ����
        player.GetComponent<Player>().stopMove = false;
    }
}
