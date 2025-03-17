using System.Collections;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    // 카메라
    private Camera mainCamera;

    // 방 리셋
    private GameObject[] objects;
    private Vector3[] objectPositions;

    // 페이드
    private GameObject fade;

    // 거리
    public float targetDistance;

    private void Start()
    {
        // 카메라
        mainCamera = Camera.main;


        // 오브젝트 좌표 저장
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
        // 1. 플레이어 이동 중지
        player.GetComponent<Player>().stopMove = true;

        // 2. 페이드 아웃
        fade.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(0.25f);

        // 3. 방 리셋
        for (int i = 0; i < objects.Length; i++)
        {
           objects[i].transform.position = objectPositions[i];
        }

        // 4. 플레이어 이동
        Vector3 direction = transform.forward;
        Vector3 playerTpPosition = player.transform.position + direction * targetDistance;
        player.transform.position = playerTpPosition;

        // 5. 카메라 이동
        Vector3 cameraPosition = mainCamera.transform.position;
        cameraPosition.x += direction.x * 20;
        cameraPosition.z += direction.z * 20;
        mainCamera.transform.position = cameraPosition;

        // 6. 페이드 인
        fade.GetComponent<Animator>().SetTrigger("FadeIn");
        yield return new WaitForSeconds(0.25f);

        // 7. 플레이어 이동 복구
        player.GetComponent<Player>().stopMove = false;
    }
}
