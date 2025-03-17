using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class enemy3 : MonoBehaviour
{
    public Animator animator;
    public float maxHealth = 50;
    public float currentHealth;
    Dusman3AI enemy3ai;
    public GameObject ringPrefab;

    void Start()
    {
        currentHealth = maxHealth;
        enemy3ai = GetComponent<Dusman3AI>();
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("Hurt");
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        
        Debug.Log("Enemy Died!");
        animator.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 2f);
        this.enabled = false;
        int randomNumber = UnityEngine.Random.Range(1, 3);
        if (randomNumber == 1) {
            Instantiate(ringPrefab, transform.position - new Vector3(0, 0.7f, 0),transform.rotation);
        }
    }
}
