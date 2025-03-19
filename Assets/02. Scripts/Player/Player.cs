using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 체력
    public float maxHP;
    public float hp;

    // 무적
    private bool god;

    // 이동
    public float moveSpeed = 5f;
    public bool stopMove = false;
    private Rigidbody rb;

    // 입력
    private float playerX, playerZ;

    // 회전
    private Quaternion targetRotation;

    public GameObject animPivot;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        hp = Mathf.Min(hp, maxHP);

        // 입력
        playerX = Input.GetAxisRaw("Horizontal");
        playerZ = Input.GetAxisRaw("Vertical");

        animPivot.GetComponent<Animator>().SetBool("isWalk", (playerX != 0 || playerZ != 0));
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

        // 이동
        Vector3 moveVelocity = inputDirection * moveSpeed;
        moveVelocity.y = rb.velocity.y;
        rb.velocity = moveVelocity;
    }

    public void SetDamage(float damage)
    {
        StartCoroutine(Damage(damage));
    }

    public IEnumerator Damage(float damage)
    {
        if (god)
            yield break;

        god = true;
        hp -= damage;
        for (int i = 0; i < 6; i++)
        {
            animPivot.SetActive(!animPivot.activeSelf);
            yield return new WaitForSeconds(0.2f);
        }
        god = false;
    }
}
