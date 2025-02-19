using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] RawImage soundIcon;
    [SerializeField] List<Texture> audioImages;

    private float lastVolume;

    void Start()
    {
        lastVolume = AudioListener.volume;
    }

    void Update()
    {
        AudioListener.volume = slider.value;
        UpdateIcon();
    }

    public void Mute()
    {
        if (slider.value > 0)
        {
            lastVolume = slider.value;
            slider.value = 0;
        }
        else
        {
            slider.value = lastVolume;
        }
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
