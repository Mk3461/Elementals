using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
  
    public string mainMenuname = "girisEkran";
    public string level1name = "level1";
    public string level2name = "level2";
    public string level3name = "level3";
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("girisEkran");
    }

    public void LoadLevel1()
    {
  
        SceneManager.LoadScene(level1name);
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene(level2name);
    }
    public void LoadLevel3()
    {
        SceneManager.LoadScene("level3");
    }
}


