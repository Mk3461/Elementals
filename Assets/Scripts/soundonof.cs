using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class soundonof : MonoBehaviour
{
    private Sprite soundOnImage;
    public Sprite soundOfImage;
    public Button button;
    public bool isOn = true;

    //public AudioSource audioSource;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        soundOnImage = button.image.sprite;
    }
    void Update()
    {

    }
    public void ButtonClicked()
    {
        if (isOn)
        {
            button.image.sprite = soundOfImage;
            isOn = false;
            //audioSource.mute=true;
            AudioListener.pause = true;
        }
        else
        {
            button.image.sprite = soundOnImage;
            isOn = true;
            //audioSource.mute=false;
            AudioListener.pause = false;
        }
    }
}
