using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // �̵�
    public float moveSpeed = 5f;
    private Rigidbody rb;

    // �Է�
    private float playerX, playerZ;

    // ȸ��
    private Quaternion targetRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // �Է�
        playerX = Input.GetAxisRaw("Horizontal");
        playerZ = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // ����
        Vector3 inputDirection = new Vector3(playerX, 0, playerZ).normalized;

        // ȸ��
        if (inputDirection.magnitude != 0)
        {
            targetRotation = Quaternion.LookRotation(inputDirection);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);

        // �ӵ�
        Vector3 moveVelocity = inputDirection * moveSpeed;
        moveVelocity.y = rb.velocity.y;
        rb.velocity = moveVelocity;
    }
}
