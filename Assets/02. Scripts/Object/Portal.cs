using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    // ī�޶�
    private Camera mainCamera;

    // ��ġ
    public Vector3 playerPosition;
    public Vector3 cameraPosition;

    private void Start()
    {
        // ī�޶�
        mainCamera = Camera.main;
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

        // 4. �÷��̾� �̵�
        player.transform.position = playerPosition;

        // 5. ī�޶� �̵�
        mainCamera.transform.position = cameraPosition;

        // 6. ���̵� ��
        Fade.instance.FadeIn();
        yield return new WaitForSeconds(0.25f);

        // 7. �÷��̾� �̵� ����
        player.GetComponent<Player>().stopMove = false;
    }
}
