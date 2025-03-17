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
        // Varsayýlan deðer 0 olarak ayarlandý, eðer "selectedCharacter" anahtarý yoksa 0 kullanýlacak
        int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter", 0);

        // Seçilen karakter indeksinin geçerli olup olmadýðýný kontrol edelim
        if (selectedCharacter < 0 || selectedCharacter >= characterPrefabs.Length)
        {
            Debug.LogError("Seçilen karakter indeksi geçersiz!");
            return;
        }

        GameObject prefab = characterPrefabs[selectedCharacter];
        GameObject clone = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);

        // Debug log ile instantiate edilen karakterin adýný yazdýrýyoruz
        Debug.Log("Karakter instantiate edildi: " + clone.name);

        // Karakterin aktif olup olmadýðýný kontrol ediyoruz
        if (!clone.activeSelf)
        {
            clone.SetActive(true);
        }

        // Renderer bileþenlerinin aktif olup olmadýðýný kontrol ediyoruz
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
