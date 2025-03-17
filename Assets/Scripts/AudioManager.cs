using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioSource audioSource;
    private bool isMuted;
    public List<string> scenesToMute = new List<string> { "level2", "gameoverscene" };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        isMuted = PlayerPrefs.GetInt("isSoundOn", 1) == 0;
        UpdateAudioState();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scenesToMute.Contains(scene.name))
        {
            audioSource.Pause();
        }
        else if (!isMuted)
        {
            audioSource.UnPause();
        }
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        PlayerPrefs.SetInt("isSoundOn", isMuted ? 0 : 1);
        UpdateAudioState();
    }

    private void UpdateAudioState()
    {
        AudioListener.pause = isMuted;
        if (isMuted)
        {
            audioSource.Pause();
        }
        else if (!scenesToMute.Contains(SceneManager.GetActiveScene().name))
        {
            audioSource.UnPause();
        }
    }

    public bool IsMuted()
    {
        return isMuted;
    }
}
