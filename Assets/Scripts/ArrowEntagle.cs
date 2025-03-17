using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowEntagle : MonoBehaviour
{
    public GameObject impactEffect;
    public float speed = 30f;
    public Rigidbody2D rb;
    public LayerMask enemyLayer;
    private float damage = 15;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D enemy)
    {
        for (int i = 0; i < 3; i++)
        {
            StartCoroutine(Hasar());
            if (enemy.name == "Enemy1") { enemy.GetComponent<enemy1>().TakeDamage(damage); }
            if (enemy.name == "Enemy2") { enemy.GetComponent<enemy2>().TakeDamage(damage); }
            if (enemy.name == "Enemy3") { enemy.GetComponent<enemy3>().TakeDamage(damage); }
            //if (enemy.name == "Enemy4") { enemy.GetComponent<enemy4>().TakeDamage(damage); }
            //if (enemy.name == "Enemy5") { enemy.GetComponent<enemy5>().TakeDamage(damage); }
            //if (enemy.name == "FireBall(Clone)") { enemy.GetComponent<FireBall>().TakeDamage(damage); }
        }
        Destroy(gameObject);
        GameObject instantiate = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(instantiate, 1f);
    }
    IEnumerator Hasar()
    {
        yield return new WaitForSeconds(1.2f);
    }
    

}
