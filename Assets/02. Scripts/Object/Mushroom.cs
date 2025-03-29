using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public float maxHP = 3;
    public float currentHP = 3;

    private float detectionRange = 20f;
    private Transform player;

    public Animator model;
    private bool death = false;

    private void Start()
    {
        FindPlayer();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange && !death)
        {
            MoveTowardsPlayer();
        }
    }

    private void FindPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += 0.4f * Time.deltaTime * direction;
        transform.LookAt(player);
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
