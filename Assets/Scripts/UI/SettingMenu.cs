﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{

    [SerializeField]
    private Slider volumeSlider;

    [SerializeField]
    private Slider sfxSlider;

    [SerializeField]
    private AudioSource uiClick;

    void Start()
    {
        volumeSlider.value = Settings.Volume;
        sfxSlider.value = Settings.SFX;

        volumeSlider.onValueChanged.AddListener(delegate {
            Settings.Volume = volumeSlider.value;
        });

        sfxSlider.onValueChanged.AddListener(delegate {
            uiClick.volume = sfxSlider.value;
            uiClick.Play();
            Settings.SFX = sfxSlider.value;
        });
    }

}
