using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 체력
    public float hp = 100f;

    // 이동
    public float moveSpeed = 5f;
    public bool stopMove = false;
    private Rigidbody rb;

    // 입력
    private float playerX, playerZ;

    // 회전
    private Quaternion targetRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 입력
        playerX = Input.GetAxisRaw("Horizontal");
        playerZ = Input.GetAxisRaw("Vertical");

        GetComponentInChildren<Animator>().SetBool("isWalk", (playerX != 0 || playerZ != 0));
    }

    void FixedUpdate()
    {
        if (stopMove)
        {
            playerX = 0;
            playerZ = 0;
        }

        // 방향
        Vector3 inputDirection = new Vector3(playerX, 0, playerZ).normalized;

        // 회전
        if (inputDirection.magnitude != 0)
        {
            targetRotation = Quaternion.LookRotation(inputDirection);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);

        // 속도
        Vector3 moveVelocity = inputDirection * moveSpeed;
        moveVelocity.y = rb.velocity.y;
        rb.velocity = moveVelocity;
    }
}
