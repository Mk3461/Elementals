using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeoRing : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Leaf_Ranger") { Debug.Log("bimbom"); }
        if (collision.name == "Fire_Knight") { collision.GetComponent<Fire_Knight_Combat>().normalAttackDamage += 10; }
        if (collision.name == "Wind_Hashashin") { collision.GetComponent<Wind_Hashashin_Combat>().normalAttackDamage += 10; }
        if (collision.name == "Water_Priestess") { collision.GetComponent<Water_Priestess_Combat>().normalAttackDamage += 10; }
        Destroy(gameObject);
    }
}
