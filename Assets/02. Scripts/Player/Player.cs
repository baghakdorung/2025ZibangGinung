using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 체력
    public float maxHP;
    public float hp;

    // 산소
    public float maxOxygen;
    public float oxygen;

    // 무적
    private bool god;

    // 이동
    public float moveSpeed = 5f;
    public float weightSlow = 1f;
    public bool stopMove = false;
    private Rigidbody rb;

    // 입력
    private float playerX, playerZ;

    // 회전
    private Quaternion targetRotation;

    // 애니메이션
    public GameObject animPivot;

    // 가방
    public List<string> inventory = new(8);
    public List<int> weight = new(8);

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        maxOxygen = 300 + GameManager.instance.shopOxygen * 50;
        oxygen = maxOxygen;
    }

    void Update()
    {
        hp = Mathf.Min(hp, maxHP);
        oxygen = Mathf.Min(oxygen, maxOxygen);

        weightSlow = 1;
        if (weight.Sum() > 100)
        {
            weightSlow -= Mathf.Min(0.5f, (weight.Sum() - 100) * 0.01f);
        }

        // 입력
        playerX = Input.GetAxisRaw("Horizontal");
        playerZ = Input.GetAxisRaw("Vertical");

        animPivot.GetComponent<Animator>().SetBool("isWalk", playerX != 0 || playerZ != 0);

        oxygen -= Time.deltaTime;
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
        Vector3 moveVelocity = inputDirection * moveSpeed * weightSlow;
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
