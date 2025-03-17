using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] characters;
    private int selectedCharacterIndex;

    public void Start()
    {
        // Seçilen karakterin indeksini alýn
        selectedCharacterIndex = PlayerPrefs.GetInt("selectedCharacterIndex", 0);

        // Seçilen karakterin örneðini oluþturun veya etkinleþtirin
        for (int i = 0; i < characters.Length; i++)
        {
            if (i == selectedCharacterIndex)
            {
                characters[i].SetActive(true);
            }
            else
            {
                characters[i].SetActive(false);
            }
        }
    }
}
