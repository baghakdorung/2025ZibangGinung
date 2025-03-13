using UnityEngine;

public class Teleport : MonoBehaviour
{
    // ī�޶�
    private Camera mainCamera;

    // �� ����
    private GameObject[] objects;
    private Vector3[] objectPositions;

    private void Start()
    {
        mainCamera = Camera.main;

        objects = GameObject.FindGameObjectsWithTag("Object");
        objectPositions = new Vector3[objects.Length];

        for (int i = 0; i < objects.Length; i++)
        {
            objectPositions[i] = objects[i].transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 direction = transform.forward;

            // �÷��̾� �̵�
            Vector3 playerTeleportPosition = transform.position + direction * 13;
            other.transform.position = playerTeleportPosition;

            // ī�޶� �̵�
            Vector3 cameraPosition = mainCamera.transform.position;
            cameraPosition.x += direction.x * 20;
            cameraPosition.z += direction.z * 20;
            mainCamera.transform.position = cameraPosition;

            ResetRoom();
        }
    }

    private void ResetRoom()
    {
        for (int i = 0; i < objects.Length; i++)
        {
           objects[i].transform.position = objectPositions[i];
        }
    }
}
