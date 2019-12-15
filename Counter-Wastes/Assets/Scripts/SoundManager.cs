using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField]
    private AudioSource musicSource;

    [SerializeField]
    private AudioSource sfxSource;

    [SerializeField]
    private Slider sfxSlider;

    [SerializeField]
    private Slider musicSlider;

    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>("sounds") as AudioClip[];

        foreach (AudioClip clip in clips)
        {
            audioClips.Add(clip.name, clip);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            PlaySFX("ammo_pickup");
        }
    }

    public void PlaySFX(string name)
    {
        sfxSource.PlayOneShot(audioClips[name]);
    }


    public void Hitsound(string index)
    {
        if (index == "monster")
        {
            int HitsoundIndex = Random.Range(0, 4);
            string type = string.Empty;
            switch (HitsoundIndex)
            {
                case 0:
                    type = "flesh_impact_bullet1";
                    break;
                case 1:
                    type = "flesh_impact_bullet3";
                    break;
                case 2:
                    type = "flesh_impact_bullet4";
                    break;
                case 3:
                    type = "flesh_impact_bullet5";
                    break;
                default:
                    break;
            }
            PlaySFX((string)type);
        }
        if (index == "tower")
        {
            int HitsoundIndex = Random.Range(0, 4);
            string type = string.Empty;
            switch (HitsoundIndex)
            {
                case 0:
                    type = "body_medium_impact_hard3";
                    break;
                case 1:
                    type = "body_medium_impact_hard4";
                    break;
                case 2:
                    type = "body_medium_impact_hard5";
                    break;
                case 3:
                    type = "body_medium_impact_hard6";
                    break;
                default:
                    break;
            }
            PlaySFX((string)type);
        }
    }

    public void DeathSound(string index)
    {
        if (index == "monster")
        {
            PlaySFX("flesh_squishy_impact_hard4");
        }
        if (index == "tower")
        {
            int DeathSoundIndex = Random.Range(0, 2);
            string type = string.Empty;
            switch (DeathSoundIndex)
            {
                case 0:
                    type = "body_medium_break3";
                    break;
                case 1:
                    type = "body_medium_break2";
                    break;
                default:
                    break;
            }
            PlaySFX((string)type);
        }
    }

    public void UpdateVolume()
    {
        musicSource.volume = musicSlider.value;
        sfxSource.volume = sfxSlider.value;
    }


    public void ClickSound()
    {
        PlaySFX("buttonclickrelease");
    }

    public void RollSound()
    {
        PlaySFX("buttonrollover");
    }

    public void MenuPopMusic(string order)
    {
        if (order == "cut")
        {
            musicSource.pitch = 0f;
        }
        if (order == "go")
        {
            musicSource.pitch = 1f;
        }
    }

    public void TowerButtonClickSound()
    {
        if (!GameManager.Instance.WaveActive)
        {
            PlaySFX("wpn_denyselect");
        }
        else
        {
            PlaySFX("wpn_select");
        }
        
    }

    public void TowerButtonRollSound()
    {
        PlaySFX("wpn_moveselect");
    }

}
