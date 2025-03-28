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

    // 공격
    private bool isAttack = false;
    public float damage = 1;
    public Transform attackRange;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        maxOxygen = 300 + GameManager.instance.shopOxygen * 50;
        oxygen = maxOxygen;
    }

    void Update()
    {
        // 스탯
        hp = Mathf.Min(hp, maxHP);
        oxygen = Mathf.Min(oxygen, maxOxygen);

        // 무게
        weightSlow = 1;
        if (weight.Sum() > 100)
        {
            weightSlow -= Mathf.Min(0.5f, (weight.Sum() - 100) * 0.01f);
        }

        // 입력
        playerX = Input.GetAxisRaw("Horizontal");
        playerZ = Input.GetAxisRaw("Vertical");

        animPivot.GetComponent<Animator>().SetBool("isWalk", playerX != 0 || playerZ != 0);

        // 산소
        oxygen -= Time.deltaTime;


        if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(Attack() );
        }
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

    public IEnumerator Attack()
    {
        if (!isAttack)
        {
            isAttack = true;

            animPivot.GetComponent<Animator>().SetTrigger("attack");
            yield return new WaitForSeconds(0.25f);

            Vector3 pos = attackRange.position; //감지할 부분의 중앙값
            Vector3 scale = attackRange.lossyScale / 2f; //크기 (절반크기 사용해야함)

            Collider[] objs = Physics.OverlapBox(pos, scale, attackRange.rotation);

            foreach(Collider obj in objs)
            {
                if (obj.TryGetComponent(out Mushroom enemy))
                {
                    enemy.GetDamage(damage);
                }
            }

            isAttack = false;
        }
    }
}
