using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public Animator animator;
    public float speed = 5f;
    public float damage = 10;
    public float playersDamage = 25;
    public float currentHealth;
    public float maxHealth;
    Fire_Knight_Combat p1;
    Water_Priestess_Combat p2;
    Wind_Hashashin_Combat p3;
    Bow p4;

    private Transform target;



    void Start()
    {
        currentHealth = maxHealth;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector2 direction = (Vector2)target.position - (Vector2)transform.position - new Vector2(0, 1.4f);
            direction.Normalize();// birim vektör olmayý saðlar.
            transform.Translate(direction * speed * Time.deltaTime, Space.World);//direction yönüne doðru hareketi saðlar.
            animator.SetBool("Hurt", false);
        }
    }
    public void TakeDamage(float playersDamage)
    {


        currentHealth -= playersDamage;
        animator.SetBool("Hurt", true);

        if (currentHealth <= 0)
        {
            Die();
        }


    }
    void Die()
    {
        animator.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 0.3f);
        this.enabled = false;
    }
    void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison.CompareTag("Player"))
        {

            if (collison.GetComponent<Fire_Knight_Combat>()!=null)
            {

                collison.GetComponent<Fire_Knight_Combat>().takeDamage(damage);

                animator.SetBool("Explosion", true);
                Destroy(gameObject, 1f);

            }
            else if (collison.GetComponent<Bow>() != null)
            {
                collison.GetComponent<Bow>().takeDamage(damage);
                animator.SetBool("Explosion", true);
                Destroy(gameObject,1f);

            }
            else if (collison.GetComponent<Water_Priestess_Combat>() != null)
            {
                collison.GetComponent<Water_Priestess_Combat>().takeDamage(damage);
                animator.SetBool("Explosion", true);
                Destroy(gameObject,1f);
            }
            else if (collison.GetComponent<Wind_Hashashin_Combat>() != null)
            {
                collison.GetComponent<Wind_Hashashin_Combat>().takeDamage(damage);
                animator.SetBool("Explosion", true);
                Destroy(gameObject, 1f);
            }
        }
    }
}

