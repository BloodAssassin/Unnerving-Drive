using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] RawImage soundIcon;
    [SerializeField] List<Texture> audioImages;

    private float lastVolume;
    private float mutedVolume;

    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("Volume");

        lastVolume = AudioListener.volume;
        mutedVolume = lastVolume;
        slider.value = lastVolume;
    }

    void Update()
    {
        AudioListener.volume = slider.value;
        UpdateIcon();

        // Save Volume
        if (lastVolume != AudioListener.volume)
        {
            PlayerPrefs.SetFloat("Volume", AudioListener.volume);
            lastVolume = AudioListener.volume;
        }
    }

    public void Mute()
    {
        if (slider.value > 0)
        {
            mutedVolume = slider.value;
            slider.value = 0;
        }
        else if (mutedVolume == 0)
        {
            slider.value = 1;
            mutedVolume = 1;
        }
        else
        {
            slider.value = mutedVolume;
        }

        PlayerPrefs.SetFloat("Volume", AudioListener.volume);
    }

    private void UpdateIcon()
    {
        if (slider.value > 0.7)
        {
            soundIcon.texture = audioImages[3];
        }
        else if (slider.value > 0.3)
        {
            soundIcon.texture = audioImages[2];
        }
        else if (slider.value > 0)
        {
            soundIcon.texture = audioImages[1];
        }
        else
        {
            soundIcon.texture = audioImages[0];
        }
    }
}
