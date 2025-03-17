using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    private Transform target;
    private float distance;
    public float followSpeed;
    public LayerMask playerLayers;
    public Transform attackPoint;
    public float attackRange = 2f;
    public float damage = 15f;
    public Rigidbody2D rb;
    void Start()
    {
        Physics2D.queriesStartInColliders = false;
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        distance = target.position.x - transform.position.x;
        if (distance < 6)
        {
            EnemyFollow();
        }
    }
    public void EnemyFollow()
    {
        if (target != null)
        {
            // Rakibin pozisyonuna doðru hareket et
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * followSpeed * Time.deltaTime;

            // Düþmanýn yönünü hedefe doðru çevir
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        }
    }
    public void EnemyAttack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayers);
        foreach (var player in hitPlayer)
        {
            if (player.GetComponent<Fire_Knight_Combat>() != null) { player.GetComponent<Fire_Knight_Combat>().takeDamage(damage); }
            else if (player.GetComponent<Bow>() != null) { player.GetComponent<Bow>().takeDamage(damage); }
            else if (player.GetComponent<Water_Priestess_Combat>() != null) { player.GetComponent<Water_Priestess_Combat>().takeDamage(damage); }
            else if (player.GetComponent<Wind_Hashashin_Combat>() != null) { player.GetComponent<Wind_Hashashin_Combat>().takeDamage(damage); }
        }
    }
}
