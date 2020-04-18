using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{

    [SerializeField]
    private Slider volumeSlider;

    [SerializeField]
    private Slider sfxSlider;

    void Start()
    {
        volumeSlider.value = Settings.Volume;
        sfxSlider.value = Settings.SFX;

        volumeSlider.onValueChanged.AddListener(delegate {
            Settings.Volume = volumeSlider.value;
        });

        sfxSlider.onValueChanged.AddListener(delegate {
            Settings.SFX = sfxSlider.value;
        });
    }

}
