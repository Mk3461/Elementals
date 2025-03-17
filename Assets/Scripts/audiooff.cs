using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audiooff : MonoBehaviour
{
    public AudioSource audioSource;
    public soundonof sound;

    void Start()
    {
        if (sound != null)
        {
            audioSource.mute = !sound.isOn; // !sound.isOn olmalý
        }
        else
        {
            Debug.LogError("soundonof script is not assigned.");
        }
    }
}