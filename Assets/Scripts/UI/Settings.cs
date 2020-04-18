using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    public static float Volume
    {
        set
        {
            PlayerPrefs.SetFloat("Volume", value);
            AudioListener.volume = value;

        }
        get
        {
            return PlayerPrefs.GetFloat("Volume", 1);
        }
    }

    public static bool Vibration
    {
        set
        {
            PlayerPrefs.SetInt("Vibration", value == true ? 1 : 0);
            if (value)
            {
                Handheld.Vibrate();
            }

        }
        get
        {
            return PlayerPrefs.GetInt("Vibration", 1) == 1;
        }
    }

}
