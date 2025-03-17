using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject impactEffect;
    public float speed = 30f;
    public Rigidbody2D rb;
    public LayerMask enemyLayer;
    public float damage = 25;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        
    }
    private void OnTriggerEnter2D(Collider2D enemy)
    {
        
        if (enemy.GetComponent<Boss>() != null) { enemy.GetComponent<Boss>().TakeDamage(damage); }
        if (enemy.GetComponent<enemy1>() != null) { enemy.GetComponent<enemy1>().TakeDamage(damage); }
        if (enemy.GetComponent<enemy2>() != null) { enemy.GetComponent<enemy2>().TakeDamage(damage); }
        if (enemy.GetComponent<enemy3>() != null) { enemy.GetComponent<enemy3>().TakeDamage(damage); }
        if (enemy.GetComponent<enemy4>() != null) { enemy.GetComponent<enemy4>().TakeDamage(damage); }
        if (enemy.GetComponent<enemy5>() != null) { enemy.GetComponent<enemy5>().TakeDamage(damage); }
        if (enemy.GetComponent<FireBall>() != null) { enemy.GetComponent<FireBall>().TakeDamage(damage); }
        
        Destroy(gameObject);
        GameObject instantiate = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(instantiate, 1f);
    }

    

}
