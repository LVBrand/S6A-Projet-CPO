using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtonsSounds : MonoBehaviour
{
    public AudioSource button_AudioSource;
    public AudioClip button_click_sound;
    public AudioClip button_roll_sound;

    public void ClickSound()
    {
        button_AudioSource.PlayOneShot(button_click_sound);
    }
    
    public void RollSound()
    {
        button_AudioSource.PlayOneShot(button_roll_sound);
    }
}
