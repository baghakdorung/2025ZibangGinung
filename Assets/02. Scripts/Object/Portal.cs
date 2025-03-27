using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    // 카메라
    private Camera mainCamera;

    // 위치
    public Vector3 playerPosition;
    public Vector3 cameraPosition;

    private void Start()
    {
        // 카메라
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
        // 1. 플레이어 이동 중지
        player.GetComponent<Player>().stopMove = true;

        // 2. 페이드 아웃
        Fade.instance.FadeOut();
        yield return new WaitForSeconds(0.25f);

        // 4. 플레이어 이동
        player.transform.position = playerPosition;

        // 5. 카메라 이동
        mainCamera.transform.position = cameraPosition;

        // 6. 페이드 인
        Fade.instance.FadeIn();
        yield return new WaitForSeconds(0.25f);

        // 7. 플레이어 이동 복구
        player.GetComponent<Player>().stopMove = false;
    }
}
