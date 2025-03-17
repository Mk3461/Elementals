using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class Dusman3AI : MonoBehaviour
{


    private Animator animator;
    Collider2D playerCollider;
    public GameObject arrowPrefab;
    public Transform arrowPos;
    private float timer;
    GameObject target;
    
    
    public Transform PlayerPos;

    void Start()
    {
        animator= GetComponent<Animator>();
        
    }



    void Update()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        Vector3 targetPos = target.transform.position;
        float distance = Vector2.Distance(transform.position,target.transform.position);
        if (distance < 13)
        {
            timer += Time.deltaTime;
            if (timer > 5)
            {   
                Shoot();
                
                
            }
            
        }
    }
    void Shoot()
    {
            animator.SetTrigger("Attack");
            timer = 0;
            
    }
    
       void ArrowFirePoint()
        {
            Instantiate(arrowPrefab,arrowPos.position,arrowPos.rotation);//okun çýkýþ animasyonuna koymak için
        }
}

