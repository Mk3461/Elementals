using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadCharacter : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public Transform spawnPoint;
    public TMP_Text label;

    void Start()
    {
        // Varsay�lan de�er 0 olarak ayarland�, e�er "selectedCharacter" anahtar� yoksa 0 kullan�lacak
        int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter", 0);

        // Se�ilen karakter indeksinin ge�erli olup olmad���n� kontrol edelim
        if (selectedCharacter < 0 || selectedCharacter >= characterPrefabs.Length)
        {
            Debug.LogError("Se�ilen karakter indeksi ge�ersiz!");
            return;
        }

        GameObject prefab = characterPrefabs[selectedCharacter];
        GameObject clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);

        // Debug log ile instantiate edilen karakterin ad�n� yazd�r�yoruz
        Debug.Log("Karakter instantiate edildi: " + clone.name);

        // Karakterin aktif olup olmad���n� kontrol ediyoruz
        if (!clone.activeSelf)
        {
            clone.SetActive(true);
        }

        // Renderer bile�enlerinin aktif olup olmad���n� kontrol ediyoruz
        Renderer[] renderers = clone.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            if (!renderer.enabled)
            {
                renderer.enabled = true;
            }
        }

        label.text = prefab.name;
    }
}
