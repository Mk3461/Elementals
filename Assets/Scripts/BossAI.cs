/*
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public Vector2 pos1;
    public Vector2 pos2;
    public float leftrightspeed;
    private float oldPosition;
    public float distance;
    private Transform target;
    public float followSpeed;
    private float originalFollowSpeed;
    public LayerMask playerLayer;
    public Transform attackPoint;
    private float attackRange = 2f;
    private Rigidbody2D rb;
    private float speed = 3f;

    private Animator anim;
    public float damage = 10;
    Vector3 demonPos;



    // Start is called before the first frame update
    void Start()
    {
        Physics2D.queriesStartInColliders = false;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        oldPosition = transform.position.x;
        originalFollowSpeed = followSpeed;
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        demonPos = transform.position- new Vector3(0,2f,0);

    }
    void bossMove()
    {

        // Oyuncuyu takip etmesi

        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
        FlipSpriteBasedOnDirection();


    }
  


        void FollowPlayer()
        {
            Vector2 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            Vector2 target = new Vector2(playerPos.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }

        //Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
        //transform.position = Vector2.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
    
    private void FlipSpriteBasedOnDirection()
    {

        // Boss'un yönünü deðiþtirmek için
        if (transform.position.x > oldPosition)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (transform.position.x < oldPosition)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        oldPosition = transform.position.x;
    }
    IEnumerator StopForAttack()
    {

        followSpeed = 0;        // Takip hýzýný durdur
        anim.SetBool("Walking", false);
        yield return new WaitForSeconds(1f); // Saldýrý süresini ayarla

        // Oyuncunun hala saldýrý menzilinde olup olmadýðýný kontrol et
        RaycastHit2D hitEnemy = Physics2D.Raycast(demonPos, -transform.right, distance, playerLayer);
        if (hitEnemy.collider != null)
        {
            // Eðer oyuncu hala menzildeyse saldýrýya devam et
            //StartCoroutine(StopForAttack());
            AttackOnAnimation();
        }
        else
        {
            // Oyuncu menzilden çýktýysa normale dön
            anim.SetBool("Attack", false);
            anim.SetBool("Walking", true);
            followSpeed = originalFollowSpeed;
        }

    }

    void AttackOnAnimation()
    {

        //Belirlenen bölgede belirlenen çapta daire oluþturur ve dairenin çarptýðý bütün nesneleri toplar
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,playerLayer);

        //Vurulan düþmanlarý tutan listedeki herkese hasar uygulama
        foreach (var player in hitPlayer)
        {
            //Vurulan hasar buraya girilecek
            
                if (player.GetComponent<Fire_Knight_Combat>()!=null)
                {
                    player.GetComponent<Fire_Knight_Combat>().takeDamage(damage);
                }
                else if (player.GetComponent<Bow>() != null)
                {
                    player.GetComponent<Bow>().takeDamage(damage);

                }
                else if (player.GetComponent<Water_Priestess_Combat>() != null)
                {
                    player.GetComponent<Water_Priestess_Combat>().takeDamage(damage);
                }
                else if (player.GetComponent<Wind_Hashashin_Combat>() != null)
                {
                    player.GetComponent<Wind_Hashashin_Combat>().takeDamage(damage);

                }
            }
        }
    }

*/
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public Vector2 pos1;
    public Vector2 pos2;
    public float leftrightspeed;
    private float oldPosition;
    public float distance;
    private Transform target;
    public float followSpeed;
    private float originalFollowSpeed;
    public LayerMask playerLayer;
    public Transform attackPoint;
    private float attackRange = 2f;

    private Animator anim;
    public float damageToPlayer = 10;
    Vector3 demonPos;
    public float damage = 15f;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        Physics2D.queriesStartInColliders = false;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        oldPosition = transform.position.x;
        originalFollowSpeed = followSpeed;
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    
    void Update()
    {
        bossAi();
        demonPos = transform.position - new Vector3(0, 2f, 0);

    }
    void bossMove()
    {

        // Oyuncuyu takip etmesi

        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
        FlipSpriteBasedOnDirection();


    }
    private void bossAi()
    {
        RaycastHit2D hitEnemy = Physics2D.Raycast(demonPos, -transform.right, distance);

        if (hitEnemy.collider != null)
        {
            Debug.DrawLine(transform.position, hitEnemy.point, Color.red);
            anim.SetBool("Attack", true);

            BossFollow();
            StartCoroutine(StopForAttack());

        }
        else
        {

            Debug.DrawLine(transform.position, transform.position - transform.right * distance, Color.green);
            anim.SetBool("Attack", false);
            bossMove();
        }
    }

    void BossFollow()
    {


        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
    private void FlipSpriteBasedOnDirection()
    {

       
        if (transform.position.x > oldPosition)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (transform.position.x < oldPosition)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        oldPosition = transform.position.x;
    }
    IEnumerator StopForAttack()
    {
        
        followSpeed = 0;        
        anim.SetBool("Walking", false);
        yield return new WaitForSeconds(1f); 

        
        RaycastHit2D hitEnemy = Physics2D.Raycast(demonPos, -transform.right, distance, playerLayer);
        if (hitEnemy.collider != null)
        {
            
            StartCoroutine(StopForAttack());
           //AttackOnAnimation();
        }
        else
        {
            
            anim.SetBool("Attack", false);
            anim.SetBool("Walking", true);
            followSpeed = originalFollowSpeed;
        }



    }

    void AttackOnAnimation()
    {

       
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

       
        foreach (var player in hitPlayer)
        {
            if (player.GetComponent<Fire_Knight_Combat>() != null) { player.GetComponent<Fire_Knight_Combat>().takeDamage(damage); }
            if (player.GetComponent<Bow>() != null) { player.GetComponent<Bow>().takeDamage(damage); }
            if (player.GetComponent<Water_Priestess_Combat>() != null) { player.GetComponent<Water_Priestess_Combat>().takeDamage(damage); }
            if (player.GetComponent<Wind_Hashashin_Combat>() != null) { player.GetComponent<Wind_Hashashin_Combat>().takeDamage(damage); }
        }
    }

}


