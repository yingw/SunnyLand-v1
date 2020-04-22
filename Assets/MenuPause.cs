using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MenuPause : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject pauseMenu;
    
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void setVolume(float value)
    {
        audioMixer.SetFloat("MainVolume", value);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
