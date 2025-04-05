using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public float maxHP = 3;
    public float currentHP = 3;

    private GameObject player;

    public Animator model;
    private bool death = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (!death)
        {
            if (!player.GetComponent<Player>().invisible || Vector3.Distance(player.transform.position, transform.position) * transform.localScale.y <= 2)
            {
                GetComponent<Animator>().SetBool("Idle", false);
                MoveTowardsPlayer();
            }
            else
                GetComponent<Animator>().SetBool("Idle", true);
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.position += 0.4f * Time.deltaTime * direction;
        transform.LookAt(player.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !death)
        {
            collision.transform.GetComponent<Player>().SetDamage(8);
        }
    }

    public void GetDamage(float damage)
    {
        if (!death)
        {
            currentHP -= damage;
            model.SetTrigger("GetDamage");

            if (currentHP <= 0)
            {
                death = true;
                GetComponent<Animator>().SetTrigger("Death");
            }
        }
    }

    public void DestroyMushroom()
    {
        Destroy(gameObject);
    }
}
