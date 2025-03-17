using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Water_Priestess_Combat : MonoBehaviour
{
    public Healthbar healthbar;
    PlayerMovement playerMovement;
    public Animator animator;
    public LayerMask enemyLayers;
    public float normalAttackDamage = 20;
    private float burstAttackDamage = 45;
    private float specialAttackDamage = 37;
    private int healValue = 30;
    public float maxHealth = 125;
    public float currentHealth;



    //Attack 1 i�in
    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    public Transform attackPoint1;
    public Transform attackPoint2;
    


    //Heal i�in
    public float healRate = 2f;
    private float nextHealTime = 0f;


    //Attack 3 i�in
    public float attack2Rate = 2f;
    private float nextAttack2Time = 0f;
    public Transform attack2_1Point;
    public Transform attack2_2Point;


    //Special Attack i�in
    public float SpecialAttackRate = 2f;
    private float SpecialNextAttackTime = 0f;
    public Transform SpecialAttackPoint1;
    public Transform SpecialAttackPoint2;



    private void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetHealth(currentHealth, maxHealth);
        playerMovement = GetComponent<PlayerMovement>();
    }


    void Update()
    {
        //Attack cooldown i�in
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
                

            }
        }
            if (Input.GetKeyDown(KeyCode.X))
            {
                Heal();
                nextHealTime = Time.time + 1f / nextHealTime;
                

        }

        if (Time.time >= nextAttack2Time)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Attack2();
                nextAttack2Time = Time.time + 1f / attack2Rate;
                

            }
         }
        if (Time.time >= SpecialNextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                SpecialAttack();
                SpecialNextAttackTime = Time.time + 1f / SpecialAttackRate;
                

            }
        }
    }
    void Attack()

    {
        animator.SetTrigger("Attack");
        //Belirlenen b�lgede belirlenen �apta daire olu�turur ve dairenin �arpt��� b�t�n nesneleri toplar
        Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(attackPoint1.position, attackPoint2.position, enemyLayers);
        foreach (var enemy in hitEnemies)
        {
            if (enemy.GetComponent<enemy1>() != null) { enemy.GetComponent<enemy1>().TakeDamage(normalAttackDamage); }
            if (enemy.GetComponent<enemy2>() != null) { enemy.GetComponent<enemy2>().TakeDamage(normalAttackDamage); }
            if (enemy.GetComponent<enemy3>() != null) { enemy.GetComponent<enemy3>().TakeDamage(normalAttackDamage); }
            if (enemy.GetComponent<enemy4>() != null) { enemy.GetComponent<enemy4>().TakeDamage(normalAttackDamage); }
            if (enemy.GetComponent<enemy5>() != null) { enemy.GetComponent<enemy5>().TakeDamage(normalAttackDamage); }
            if (enemy.GetComponent<FireBall>() != null) { enemy.GetComponent<FireBall>().TakeDamage(normalAttackDamage); }
            if (enemy.GetComponent<Boss>() != null) { enemy.GetComponent<Boss>().TakeDamage(normalAttackDamage); }
        }

    }
    void Heal()
    {
        animator.SetTrigger("Heal");
        currentHealth += healValue;
        healthbar.SetHealth(currentHealth, maxHealth);
    }
    void Attack2()
    {
        animator.SetTrigger("Attack2");
    }
    void SpecialAttack()
    {
        animator.SetTrigger("SpecialAttack");
    }


    void SpecialAttackOnAnimaton()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(SpecialAttackPoint1.position,SpecialAttackPoint2.position, enemyLayers);
        foreach (var enemy in hitEnemies)
        {
            if (enemy.GetComponent<enemy1>() != null) { enemy.GetComponent<enemy1>().TakeDamage(specialAttackDamage); }
            if (enemy.GetComponent<enemy2>() != null) { enemy.GetComponent<enemy2>().TakeDamage(specialAttackDamage); }
            if (enemy.GetComponent<enemy3>() != null) { enemy.GetComponent<enemy3>().TakeDamage(specialAttackDamage); }
            if (enemy.GetComponent<enemy4>() != null) { enemy.GetComponent<enemy4>().TakeDamage(specialAttackDamage); }
            if (enemy.GetComponent<enemy5>() != null) { enemy.GetComponent<enemy5>().TakeDamage(specialAttackDamage); }
            if (enemy.GetComponent<FireBall>() != null) { enemy.GetComponent<FireBall>().TakeDamage(specialAttackDamage); }
            if (enemy.GetComponent<Boss>() != null) { enemy.GetComponent<Boss>().TakeDamage(specialAttackDamage); }
        }
    }
    void Attack2OnAnimation()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(attack2_1Point.position,attack2_2Point.position,enemyLayers);
        foreach (var enemy in hitEnemies)
        {
            if (enemy.GetComponent<enemy1>() != null) { enemy.GetComponent<enemy1>().TakeDamage(burstAttackDamage); }
            if (enemy.GetComponent<enemy2>() != null) { enemy.GetComponent<enemy2>().TakeDamage(burstAttackDamage); }
            if (enemy.GetComponent<enemy3>() != null) { enemy.GetComponent<enemy3>().TakeDamage(burstAttackDamage); }
            if (enemy.GetComponent<enemy4>() != null) { enemy.GetComponent<enemy4>().TakeDamage(burstAttackDamage); }
            if (enemy.GetComponent<enemy5>() != null) { enemy.GetComponent<enemy5>().TakeDamage(burstAttackDamage); }
            if (enemy.GetComponent<FireBall>() != null) { enemy.GetComponent<FireBall>().TakeDamage(burstAttackDamage); }
            if (enemy.GetComponent<Boss>() != null) { enemy.GetComponent<Boss>().TakeDamage(burstAttackDamage); }
        }
    }




    void AttackOnAnimation()
    {
        ////Belirlenen b�lgede belirlenen �apta daire olu�turur ve dairenin �arpt��� b�t�n nesneleri toplar
        //Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(attackPoint1.position, attackPoint2.position, enemyLayers);
        //foreach (var enemy in hitEnemies)
        //{
        //    if (enemy.name == "Enemy1") { enemy.GetComponent<enemy1>().TakeDamage(normalAttackDamage); }
        //    if (enemy.name == "Enemy2") { enemy.GetComponent<enemy2>().TakeDamage(normalAttackDamage); }
        //    if (enemy.name == "Enemy3") { enemy.GetComponent<enemy3>().TakeDamage(normalAttackDamage); }
        //    if (enemy.name == "Enemy4") { enemy.GetComponent<enemy4>().TakeDamage(normalAttackDamage); }
        //    if (enemy.name == "Enemy5") { enemy.GetComponent<enemy5>().TakeDamage(normalAttackDamage); }
        //    if (enemy.name == "FireBall(Clone)") { enemy.GetComponent<FireBall>().TakeDamage(normalAttackDamage); }
        //    if (enemy.name == "Demon") { enemy.GetComponent<Boss>().TakeDamage(normalAttackDamage); }
        //}
    }
    public void takeDamage(float damage)
    {
        animator.SetTrigger("Hurt");
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        animator.SetTrigger("Dead");
        playerMovement.runSpeed = 0f;
        SceneManager.LoadScene("gameoverscene");
        Destroy(gameObject);
    }




    void OnDrawGizmosSelected()
        {
        Gizmos.DrawLine(SpecialAttackPoint1.position,SpecialAttackPoint2.position);
        Gizmos.DrawLine(attackPoint1.position,attackPoint2.position);
        Gizmos.DrawLine(attack2_1Point.position, attack2_2Point.position);
    }
    }

