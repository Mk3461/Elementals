using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyArrow : MonoBehaviour
{
    //public float speed=20f;
    Fire_Knight_Combat p1;
    Water_Priestess_Combat p2;
    Wind_Hashashin_Combat p3;
    Bow p4;
    public float damage = 20f;
    public Rigidbody2D rb;
    public float force = 5f;
    private float timer;
    private PlayerMovement playerMovement;
    private Transform target;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        Vector3 direction = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position - new Vector3(0, 1.4f, 0);
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;//okun yönünün belirlemek için açı bulmamızı sağlar.(Rad2Deg radyandan açıya geçmemizi sağlar)
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }





    void Update()
    {

        timer += Time.deltaTime;
        if (timer > 7)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.GetComponent<Fire_Knight_Combat>() != null)
        {

            col.GetComponent<Fire_Knight_Combat>().takeDamage(damage);
            Destroy(gameObject);


        }
        if (col.GetComponent<Water_Priestess_Combat>() != null)
        {

            col.GetComponent<Water_Priestess_Combat>().takeDamage(damage);
            Destroy(gameObject);


        }
        if (col.GetComponent<Wind_Hashashin_Combat>() != null)
        {

            col.GetComponent<Wind_Hashashin_Combat>().takeDamage(damage);
            Destroy(gameObject);


        }
        if (col.GetComponent<Bow>() != null)
        {

            col.GetComponent<Bow>().takeDamage(damage);
            Destroy(gameObject);


        }

    }
}

