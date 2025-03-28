using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    // ü��
    public float maxHP;
    public float hp;

    // ���
    public float maxOxygen;
    public float oxygen;

    // ����
    private bool god;

    // �̵�
    public float moveSpeed = 5f;
    public float weightSlow = 1f;
    public bool stopMove = false;
    private Rigidbody rb;

    // �Է�
    private float playerX, playerZ;

    // ȸ��
    private Quaternion targetRotation;

    // �ִϸ��̼�
    public GameObject animPivot;

    // ����
    public List<string> inventory = new(8);
    public List<int> weight = new(8);

    // ����
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
        // ����
        hp = Mathf.Min(hp, maxHP);
        oxygen = Mathf.Min(oxygen, maxOxygen);

        // ����
        weightSlow = 1;
        if (weight.Sum() > 100)
        {
            weightSlow -= Mathf.Min(0.5f, (weight.Sum() - 100) * 0.01f);
        }

        // �Է�
        playerX = Input.GetAxisRaw("Horizontal");
        playerZ = Input.GetAxisRaw("Vertical");

        animPivot.GetComponent<Animator>().SetBool("isWalk", playerX != 0 || playerZ != 0);

        // ���
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

        // ����
        Vector3 inputDirection = new Vector3(playerX, 0, playerZ).normalized;

        // ȸ��
        if (inputDirection.magnitude != 0)
        {
            targetRotation = Quaternion.LookRotation(inputDirection);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);

        // �̵�
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

            Vector3 pos = attackRange.position; //������ �κ��� �߾Ӱ�
            Vector3 scale = attackRange.lossyScale / 2f; //ũ�� (����ũ�� ����ؾ���)

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
