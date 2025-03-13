using UnityEngine;

public class Teleport : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 direction = transform.forward;

            Vector3 playerTeleportPosition = transform.position + direction * 13;
            other.transform.position = playerTeleportPosition;

            // ī�޶� �̵�
            Vector3 cameraPosition = mainCamera.transform.position;
            cameraPosition.x += direction.x * 20;
            cameraPosition.z += direction.z * 20;
            mainCamera.transform.position = cameraPosition;
        }
    }
}
