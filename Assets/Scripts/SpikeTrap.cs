using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public Transform teleport;
    private float damage = 31f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(waitForHurt(collision));
        
    }
    IEnumerator waitForHurt(Collider2D collision)
    {
        collision.transform.position = teleport.transform.position;
        yield return new WaitForSeconds(0.05f);
        if (collision.GetComponent<Fire_Knight_Combat>() != null) { collision.GetComponent<Fire_Knight_Combat>().takeDamage(damage); }
        if (collision.GetComponent<Bow>() != null) { collision.GetComponent<Bow>().takeDamage(damage); }
        if (collision.GetComponent<Water_Priestess_Combat>() != null) { collision.GetComponent<Water_Priestess_Combat>().takeDamage(damage); }
        if (collision.GetComponent<Wind_Hashashin_Combat>() != null) { collision.GetComponent<Wind_Hashashin_Combat>().takeDamage(damage); }
    }
}
