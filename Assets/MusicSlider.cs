using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    Slider slider = null;

    [SerializeField]
    bool music = false;

    void Start()
    {
        slider = GetComponent<Slider>();

        if (music)
        {
            slider.value = SoundManager.soundManager.volumeMusic;
        }
        else
        {
            slider.value = SoundManager.soundManager.volumenSound;
        }
        

    }

    void Update()
    {
        if (music)
        {
            SoundManager.soundManager.SetVolumeMusic(slider.value);
        }
        else
        {
            SoundManager.soundManager.SetVolume(slider.value);
        }
    }
}
