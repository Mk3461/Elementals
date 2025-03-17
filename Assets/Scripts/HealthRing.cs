using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRing : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Leaf_Ranger") { collision.GetComponent<Bow>().maxHealth += 50;collision.GetComponent<Bow>().currentHealth += 50; }
        if (collision.name == "Fire_Knight") { collision.GetComponent<Fire_Knight_Combat>().maxHealth += 50; collision.GetComponent<Fire_Knight_Combat>().currentHealth += 50; }
        if (collision.name == "Wind_Hashashin") { collision.GetComponent<Wind_Hashashin_Combat>().maxHealth += 50; collision.GetComponent<Wind_Hashashin_Combat>().currentHealth += 50; }
        if (collision.name == "Water_Priestess") { collision.GetComponent<Water_Priestess_Combat>().maxHealth += 50; collision.GetComponent<Water_Priestess_Combat>().currentHealth += 50; }
        Destroy(gameObject);
    }
}
